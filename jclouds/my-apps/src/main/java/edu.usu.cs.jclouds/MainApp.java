package edu.usu.cs.jclouds;

//import com.google.common.base.Charsets;
import com.google.common.base.Predicates;
import com.google.common.collect.ImmutableSet;
import com.google.common.collect.Iterables;
import com.google.common.collect.Maps;
import com.google.common.io.Files;
import com.google.inject.Module;
import org.jclouds.ContextBuilder;
import org.jclouds.apis.ApiMetadata;
import org.jclouds.apis.Apis;
import org.jclouds.compute.ComputeService;
import org.jclouds.compute.ComputeServiceContext;
import org.jclouds.compute.RunNodesException;
//import org.jclouds.compute.RunScriptOnNodesException;
import org.jclouds.compute.domain.*;
import org.jclouds.domain.LoginCredentials;
import org.jclouds.providers.ProviderMetadata;
import org.jclouds.providers.Providers;
import org.jclouds.scriptbuilder.domain.Statement;
import org.jclouds.scriptbuilder.statements.login.AdminAccess;
import org.jclouds.sshj.config.SshjSshClientModule;
import org.jclouds.enterprise.config.EnterpriseConfigurationModule;
import org.jclouds.logging.slf4j.config.SLF4JLoggingModule;

import java.io.File;
//import java.io.IOException;
import java.util.Map;
import java.util.Properties;
import java.util.Set;
import java.util.concurrent.TimeUnit;

import static com.google.common.base.Charsets.UTF_8;
import static com.google.common.base.Preconditions.checkArgument;
import static com.google.common.base.Predicates.not;
import static com.google.common.collect.Iterables.concat;
import static com.google.common.collect.Iterables.contains;
import static com.google.common.collect.Iterables.getOnlyElement;
import static org.jclouds.aws.ec2.reference.AWSEC2Constants.PROPERTY_EC2_AMI_QUERY;
import static org.jclouds.aws.ec2.reference.AWSEC2Constants.PROPERTY_EC2_CC_AMI_QUERY;
import static org.jclouds.compute.config.ComputeServiceProperties.TIMEOUT_SCRIPT_COMPLETE;
//import static org.jclouds.compute.options.TemplateOptions.Builder.overrideLoginCredentials;
import static org.jclouds.compute.options.TemplateOptions.Builder.overrideLoginUser;
import static org.jclouds.compute.options.TemplateOptions.Builder.runScript;
import static org.jclouds.compute.predicates.NodePredicates.TERMINATED;
import static org.jclouds.compute.predicates.NodePredicates.inGroup;
//import static org.jclouds.scriptbuilder.domain.Statements.exec;

/**
 * Hello world!
 *
 */
public class MainApp
{
    // All actions of this app
    public static enum Action {
        ADD, DESTROY, LISTNODES;
    }

    // Map<String, ApiMetadata> -> String: the Maven Id of each API,  ApiMetadata: includes information like id, name, endpoint, etc. of the api
    public static final Map<String, ApiMetadata> allApis = Maps.uniqueIndex(Apis.viewableAs(ComputeServiceContext.class),
            Apis.idFunction());

    // Map<String, ProviderMetadata> -> String: the Maven Id of each provider,  ProviderMetadata: includes information like id, name, endpoint, etc. of the provider
    public static final Map<String, ProviderMetadata> appProviders = Maps.uniqueIndex(Providers.viewableAs(ComputeServiceContext.class),
            Providers.idFunction());

    // Immutable: Unchangeable
    // All maven Ids of APIs and providers
    public static final Set<String> allKeys = ImmutableSet.copyOf(Iterables.concat(appProviders.keySet(), allApis.keySet()));

    public static int PARAMETERS = 5;
//    public static String INVALID_SYNTAX = "Invalid number of parameters. Syntax is: provider identity credential groupName (add|exec|run|destroy)";

    public static void main(String[] args) {
    //   if (args.length < PARAMETERS)
    //       throw new IllegalArgumentException(INVALID_SYNTAX);

        checkArgument(args.length == PARAMETERS, "Invalid number of parameters. Syntax is: provider identity credential groupName %s", Action.values());

        String provider = args[0];
        String identity = args[1];
        String credential = args[2];
        String groupName = args[3];
        Action action = Action.valueOf(args[4].toUpperCase());


        // Documents: http://www.tutorialspoint.com/java/lang/system_getproperty_string.htm
        // The java.lang.System.getProperty(String key) method gets the system property indicated by the specified key.
        String minRam = System.getProperty("minRam");  //???
        String loginUser = System.getProperty("loginUser", "tulips-mbp");  //???

        // note that you can check if a provider is present ahead of time
        checkArgument(contains(allKeys, provider), "provider %s not in supported list: %s", provider, allKeys);

        // It uses saved SSH keys to identify trusted computers, without involving passwords.
        LoginCredentials login = getLoginForCommandExecution(action);

        // Connect to my Amazon account
        ComputeService compute = initComputeService(provider, identity, credential);

        try {
            switch (action) {
                case ADD:
                    System.out.printf(">> adding node to group %s%n", groupName);

                    // Default template chooses the smallest size on an operating system
                    // that tested to work with java, which tends to be Ubuntu or CentOS
                    TemplateBuilder templateBuilder = compute.templateBuilder();

                    // If you want to up the ram and leave everything default, you can
                    // just tweak minRam
                    if (minRam != null)
                        templateBuilder.minRam(Integer.parseInt(minRam));

                    //Note this will create a user with the same name as you on the
                    // node. ex. you can connect via ssh publicip
                    Statement bootInstructions = AdminAccess.standard();

                    // to run commands as root, we use the runScript option in the template ????
                    if(provider.equalsIgnoreCase("virtualbox"))
                        templateBuilder.options(overrideLoginUser(loginUser).runScript(bootInstructions));
                    else
                        templateBuilder.options(runScript(bootInstructions));

                    NodeMetadata node = getOnlyElement(compute.createNodesInGroup(groupName, 1, templateBuilder.build()));
                    System.out.printf("<< node %s: %s%n", node.getId(),
                            concat(node.getPrivateAddresses(), node.getPublicAddresses()));

                case DESTROY:
                    System.out.printf(">> destroying nodes in group %s%n", groupName);
                    // you can use predicates to select which nodes you wish to destroy.
                    Set<? extends NodeMetadata> destroyed = compute.destroyNodesMatching(//
                            Predicates.<NodeMetadata> and(not(TERMINATED), inGroup(groupName)));
                    System.out.printf("<< destroyed nodes %s%n", destroyed);
                    break;

                case LISTNODES:
                    Set<? extends ComputeMetadata> nodes = compute.listNodes();
                    System.out.printf(">> No of nodes/instances %d%n", nodes.size());
                    for (ComputeMetadata nodeData : nodes) {
                        System.out.println(">>>>  " + nodeData);

                        // Print the group of each node -> null for my node on ec2
                        //NodeMetadataImpl testNode = (NodeMetadataImpl) nodeData;
                        //System.out.println(">>>>  " + testNode.getGroup());
                    }
                    break;
            }
        } catch (RunNodesException e) {
            System.err.println("error adding node to group " + groupName + ": " + e.getMessage());
            error = 1;
//        } catch (RunScriptOnNodesException e) {
//            System.err.println("error executing " + command + " on group " + groupName + ": " + e.getMessage());
//            error = 1;
        } catch (Exception e) {
            System.err.println("error: " + e.getMessage());
            error = 1;
        } finally {
            compute.getContext().close();
            System.exit(error);
        }
    }

//    private static String getPrivateKeyFromFile(String filename) {
//        try {
//            return Files.toString(new File(filename), Charsets.UTF_8);
//        } catch (IOException e) {
//            System.err.println("Exception reading private key from '%s': " + filename);
//            e.printStackTrace();
//            System.exit(1);
//            return null;
//        }
//    }

    static int error = 0;

    private static ComputeService initComputeService(String provider, String identity, String credential) {

        // example of specific properties, in this case optimizing image list to
        // only amazon supplied
        Properties properties = new Properties();
        properties.setProperty(PROPERTY_EC2_AMI_QUERY, "owner-id=137112412989;state=available;image-type=machine");
        properties.setProperty(PROPERTY_EC2_CC_AMI_QUERY, "");
        long scriptTimeout = TimeUnit.MILLISECONDS.convert(20, TimeUnit.MINUTES);
        properties.setProperty(TIMEOUT_SCRIPT_COMPLETE, scriptTimeout + "");

        // example of injecting a ssh implementation
        Iterable<Module> modules = ImmutableSet.<Module> of(
                new SshjSshClientModule(),
                new SLF4JLoggingModule(),
                new EnterpriseConfigurationModule());

        ContextBuilder builder = ContextBuilder.newBuilder(provider)
                .credentials(identity, credential)
                .modules(modules)
                .overrides(properties);

        System.out.printf(">> initializing %s%n", builder.getApiMetadata());

        return builder.buildView(ComputeServiceContext.class).getComputeService();
    }

    private static LoginCredentials getLoginForCommandExecution(Action action) {
        try {
            String user = System.getProperty("user.name");
            String privateKey = Files.toString(
                    new File(System.getProperty("user.home") + "/.ssh/id_rsa"), UTF_8);
            return LoginCredentials.builder().
                    user(user).privateKey(privateKey).build();
        } catch (Exception e) {
            System.err.println("error reading ssh key " + e.getMessage());
            System.exit(1);
            return null;
        }
    }
}
