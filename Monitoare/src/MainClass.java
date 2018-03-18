import java.util.ArrayList;
import java.util.List;


public class MainClass {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		List<Integer> listInteger = new ArrayList<Integer>();
		Object condProd = new Object();
		Object condCons = new Object();
		Thread[] thread = new Thread[4];
		
		thread[0] = new Producer(1, listInteger, condProd, condCons);
		thread[0].start();
		thread[1] = new Consumer(2, listInteger, condProd, condCons);
		thread[1].start();
		
		for ( int i =0; i< 4; i++)
		 {
			 try {
				 thread[i].join();
			 }
			 catch(InterruptedException e )
			 {
				 e.printStackTrace();
			 }
		 }
	}
}
