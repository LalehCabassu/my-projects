package edu.usu.wr.cloudutils.aspects;

public aspect ParameterValidationAspect {
	
	pointcut methodExpectingNonNullArg(Object o):
	     execution(public * edu.usu.wr.cloudutils.BlobStoreUtil.*(.., @edu.usu.wr.cloudutils.NonNull (*), ..)) 
	     && args(o) && 
	     within(edu.usu.wr.cloudutils.BlobStoreUtil);
	
    before(Object o): methodExpectingNonNullArg(o) {
    	if (o == null) {
    		throw new IllegalArgumentException(
    				"First argument is null at " + thisJoinPoint.getSignature());
    	}
    }
}
