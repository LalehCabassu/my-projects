package edu.usu.wr.cloudutils.aspects;

import java.util.Date;

import org.apache.log4j.Logger;
import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.Signature;

public aspect PerformanceMeasurement extends BlobStoreUtilPointcuts {
	
	private Logger _logger = Logger.getLogger(PerformanceMeasurement.class);
	
	Object around(): blobStoreMethods()
	{
		Object result;
		Date startTime = new Date();
		result = proceed();
		Date endTime = new Date();
		long executionTime = endTime.getTime() - startTime.getTime();
		if (_logger.isInfoEnabled())
			afterProceed(thisJoinPoint, executionTime);
		return result;
	}
		
	private void afterProceed(JoinPoint jp, long execTime){
    	Signature sig = jp.getSignature();
        _logger.debug(">>> " + sig.getDeclaringTypeName() + "." + sig.getName() +
        				" (Execution time: " + execTime + ")"
        			 );
    }

}
