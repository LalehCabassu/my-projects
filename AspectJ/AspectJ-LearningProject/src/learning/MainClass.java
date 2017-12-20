package learning;

public class MainClass {

	static MainClass mc;
	
	public static void main(String[] args) {
		System.out.println("main(): calling goo()");
		
		mc = new MainClass();
		mc.foo();
		mc.goo();
		
	}
	
	void foo()
	{
		System.out.println("foo(): calling goo()");
		for(int i = 0; i < 3; i++)
			goo();
	}
	
	void goo()
	{
		System.out.println("in goo()");
	}

}
