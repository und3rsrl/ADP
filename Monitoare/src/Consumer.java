import java.util.List;

public class Consumer extends Thread {
	private List<Integer> listInteger;
	private Object condProd;
	private Object condCons;
	private int name;
	
	public Consumer(int i, List<Integer> list, Object condProd, Object condCons) {
		listInteger = list;
		this.condProd = condProd;
		this.condCons = condCons;
		name = i;
	}
	
	public void run() {

		while (true) {
			Deque();

			synchronized (condProd) {
				condProd.notifyAll();
			}
			
			Consume();
		}
	}
	
	synchronized public void Deque() {

		while (listInteger.size() == 0) {
			try {
				synchronized (condCons) {
					condCons.wait();
				}
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}

		listInteger.remove(0);

		System.out.println(name + "|Am scos un item");
	}
	
	private void Consume() {
		try {
			Thread.sleep(1000);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}
