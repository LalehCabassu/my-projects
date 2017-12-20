package aspects.logTime;

import org.apache.log4j.Logger;
import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.Signature;

import java.util.Calendar;
import java.util.Date;

public aspect CalculateMethodExecutionTime {
	Logger _logger = Logger.getLogger(CalculateMethodExecutionTime.class);
	protected int indentationlevel = 0;
	 
    pointcut loggingOperations():
        (execution(* *.*(..)) || execution(*.new(..)))
        		&& !execution(* *.Set*(*)) && !execution(* *.Get*())
        			&& !within(CalculateMethodExecutionTime+);
    
    Object around(): loggingOperations() {
        StringBuffer sb = new StringBuffer();
        Date beforeExec, afterExec;
        long execTime;
        if (_logger.isInfoEnabled()) {
            indentationlevel++;
            for (int i = 0, spaces = indentationlevel * 2; i <= spaces; i++) {
                sb.append(" ");
            }
            beforeLog(sb.toString(), thisJoinPoint);   
        }
        beforeExec = getTime();
        Object result;
        try {
            result = proceed();
        } finally {
            if (_logger.isInfoEnabled()) {
            	afterExec = getTime();
                execTime = afterExec.getTime() - beforeExec.getTime();
                afterLog(sb.toString(), thisJoinPoint, execTime);
                indentationlevel--;
            }
        }
        return result;
    }
    
    void beforeLog(String indent, JoinPoint jp) {
    	Signature sig = jp.getSignature();
        String line =""+ jp.getSourceLocation().getLine();
        String sourceName = jp.getSourceLocation().getWithinType().getCanonicalName();
        _logger.debug(indent + "Calling "
	                    +  sourceName
	                    +" line " +
	                    line
	                    +" to " +sig.getDeclaringTypeName() + "." + sig.getName()
        			 );
    }
 
    void afterLog(String indent, JoinPoint jp, long execTime){
    	Signature sig = jp.getSignature();
        String line =""+ jp.getSourceLocation().getLine();
        String sourceName = jp.getSourceLocation().getWithinType().getCanonicalName();
        _logger.debug(indent + "Leaving "
	                    +  sourceName
	                    +" line " +
	                    line
	                    +" to " +sig.getDeclaringTypeName() + "." + sig.getName() +
        				" (Execution time: " + execTime + ")"
        			 );
    }
    
    private Date getTime(){
		Calendar cal = Calendar.getInstance();
		return cal.getTime();	
	}
}
