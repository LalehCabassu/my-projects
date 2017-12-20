package edu.usu.cs.jclouds;

import com.google.common.base.Charsets;
import com.google.common.collect.ImmutableSet;
import com.google.common.collect.Iterables;
import com.google.common.collect.Maps;
import com.google.common.io.ByteSource;
import com.sun.xml.internal.ws.addressing.model.ActionNotSupportedException;
import org.jclouds.ContextBuilder;
import org.jclouds.apis.ApiMetadata;
import org.jclouds.apis.Apis;
import org.jclouds.blobstore.BlobStore;
import org.jclouds.blobstore.BlobStoreContext;
import org.jclouds.blobstore.domain.Blob;
import org.jclouds.blobstore.domain.StorageMetadata;
import org.jclouds.blobstore.options.ListContainerOptions;
import org.jclouds.providers.ProviderMetadata;
import org.jclouds.providers.Providers;

import java.awt.*;
import java.io.IOException;
import java.util.Map;
import java.util.Set;

import static com.google.common.base.Preconditions.checkArgument;
import static com.google.common.collect.Iterables.contains;

public class App
{
    public static final Map<String, ApiMetadata> allApis = Maps.uniqueIndex(Apis.viewableAs(BlobStoreContext.class),
            Apis.idFunction());

    public static final Map<String, ProviderMetadata> appProviders = Maps.uniqueIndex(Providers.viewableAs(BlobStoreContext.class),
            Providers.idFunction());

    public static final Set<String> allKeys = ImmutableSet.copyOf(Iterables.concat(appProviders.keySet(), allApis.keySet()));

    public static int PARAMETERS = 6;
    public static String INVALID_SYNTAX = "Invalid number of parameters. Syntax is: \"provider\" \"identity\" \"credential\" \"action\" \"container\" \"blob\"";

    public static enum Action
    {
        LISTCONTAINERS,
        LISTBLOBS,
        ADDCONTAINER,
        ADDDIRECTORY,
        ADDBLOB,
        REMOVECONTAINER,
        REMOVEDIRECTORY,
        REMOVEBLOB;
    }

    public static void main(String[] args) throws IOException {

        if (args.length < PARAMETERS)
            throw new IllegalArgumentException(INVALID_SYNTAX);

        // Args
        String provider = args[0];

        // note that you can check if a provider is present ahead of time
        checkArgument(contains(allKeys, provider), "provider %s not in supported list: %s", provider, allKeys);

        String identity = args[1];
        String credential = args[2];
        String act = args[3];
        String containerName = args[4];
        String path = args[5];

        Action action;
        if(act.equalsIgnoreCase("AddContainer"))
            action = Action.ADDCONTAINER;
        else if(act.equalsIgnoreCase("AddDirectory"))
            action = Action.ADDDIRECTORY;
        else if(act.equalsIgnoreCase("AddBlob"))
            action = Action.ADDBLOB;
        else if (act.equalsIgnoreCase("RemoveContainer"))
            action = Action.REMOVECONTAINER;
        else if(act.equalsIgnoreCase("RemoveDirectory"))
            action = Action.REMOVEDIRECTORY;
        else if (act.equalsIgnoreCase("RemoveBlob"))
            action = Action.REMOVEBLOB;
        else if (act.equalsIgnoreCase("ListBlobs"))
            action =Action.LISTBLOBS;
        else
            action = Action.LISTCONTAINERS;

        // Init
        BlobStoreContext context = ContextBuilder.newBuilder(provider)
                .credentials(identity, credential)
                .buildView(BlobStoreContext.class);

        try {
            BlobStore blobStore = context.getBlobStore();

            switch(action) {
                case ADDCONTAINER:
                    System.out.format("\nAdding '%s' container to '%s' provider...", containerName, provider);

                    // Create Container
                    blobStore.createContainerInLocation(null, containerName);

                    // parent: root -> got from the top container(bucket)  or null (will be default location)
                    // StorageMetadata topBlob = blobStore.list().iterator().next();
                    // org.jclouds.domain.Location root = topBlob.getLocation();
                    // blobStore.createContainerInLocation(root, name);

                    // It is not possible to create a container under an existing container -> You can create folders instead.

                    listContainers(blobStore);
                break;

                case ADDDIRECTORY:
                    System.out.format("\nAdding '%s' directory to '%s' container...", path, containerName);

                    blobStore.createDirectory(containerName, path);

                    listItemsInContainer(blobStore, containerName);
                    break;

                case ADDBLOB:
                    System.out.format("\nAdding '%s' blob to '%s' container...", path, containerName);
                    // Add Blob
                    ByteSource payload = ByteSource.wrap("testdata".getBytes(Charsets.UTF_8));

                    Blob blob = blobStore.blobBuilder(path)
                            .payload(payload)
                            .contentLength(payload.size())
                            .build();
                    blobStore.putBlob(containerName, blob);

                    listItemsInContainer(blobStore, containerName);


//                    // Use Provider API
//                    if (context.getBackendType().getRawType().equals(RestContext.class)) {
//                        RestContext<?, ?> rest = context.unwrap();
//                        Object object = null;
//                        if (rest.getApi() instanceof S3Client) {
//                            RestContext<S3Client, S3AsyncClient> providerContext = context.unwrap();
//                            object = providerContext.getApi().headObject(name, name);
//                        } else if (rest.getApi() instanceof SwiftClient) {
//                            RestContext<SwiftClient, SwiftAsyncClient> providerContext = context.unwrap();
//                            object = providerContext.getApi().getObjectInfo(name, name);
//                        } else if (rest.getApi() instanceof AzureBlobClient) {
//                            RestContext<AzureBlobClient, AzureBlobAsyncClient> providerContext = context.unwrap();
//                            object = providerContext.getApi().getBlobProperties(name, name);
//                        } else if (rest.getApi() instanceof AtmosClient) {
//                            RestContext<AtmosClient, AtmosAsyncClient> providerContext = context.unwrap();
//                            object = providerContext.getApi().headFile(name + "/" + name);
//                        }
//                        if (object != null) {
//                            System.out.println(object);
//                        }
//                    }
                    break;

                case REMOVECONTAINER:
                    System.out.format("\nRemoving '%s' container from '%s' provider...", containerName, provider);

                    // Delete all the content of the container
                    blobStore.deleteContainer(containerName);

                    listContainers(blobStore);
                    break;

                case REMOVEDIRECTORY:
                    System.out.format("\nRemoving '%s' directory from '%s' container...", path, containerName);

                    blobStore.deleteDirectory(containerName, path);

                    listItemsInContainer(blobStore, containerName);
                    break;

                case REMOVEBLOB:
                    System.out.format("\nRemoving '%s' blob from '%s' container...", path, containerName);

                    blobStore.removeBlob(containerName, path);

                    listItemsInContainer(blobStore, containerName);
                    break;

                case LISTBLOBS:
                    System.out.format("\nList of all directories and blobs in '%s' container...", containerName);

                    listItemsInContainer(blobStore, containerName);
                    break;

                default:
                    System.out.format("\nList of all containers in '%s' provider...", provider);

                    listContainers(blobStore);
                    break;
            }

        } finally {
            // Close connecton
            context.close();
            System.exit(0);
        }

    }

    private static void listContainers(BlobStore blobStore)
    {
        // List Container Metadata
        System.out.format("\nAll containers: %d\n", blobStore.list().size());
        for (StorageMetadata resourceMd : blobStore.list())
            System.out.println(resourceMd);
    }

    private static void listItemsInContainer(BlobStore blobStore, String containerName)
    {
        // List all folders and files in the container
        System.out.format("\nAll %d items:\n", blobStore.countBlobs(containerName));
        for (StorageMetadata resourceMd : blobStore.list(containerName))
            System.out.println(resourceMd);
    }
}

