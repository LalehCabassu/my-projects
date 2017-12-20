
package learning;

import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.reflect.CodeSignature;

aspect AspectClass {
	pointcut fooExecution(): call(void MainClass.foo());

	void around(): cflow(fooExecution()) && call(void MainClass.goo())
	{
		System.out.println("In aspect...");
		proceed();
	}
}