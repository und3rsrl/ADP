import java.util.List;

public class Producer extends Thread{
	
	private int count = 0;
	private List<Integer> listInteger;
	private Object condProd;
	private Object condCons;
	private int name;
	
	public Producer(int i, List<Integer> list, Object condProd, Object condCons) {
		listInteger = list;
		this.condProd = condProd;
		this.condCons = condCons;
		name = i;
	}
	
	public void run() {

		while (true) {
			int nr = Produce();

			Add(nr);

			synchronized (condCons) {
				condCons.notifyAll();
			}
		}
	}
	
	private int Produce() {
		try {
			Thread.sleep(100);
			count += 1;
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return count;
	}
	
	synchronized private void Add(int nr)
	{
		while (listInteger.size() == 5) {

			try {
				synchronized (condProd) {
					condProd.wait();
				}
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}

		listInteger.add(nr);

		System.out.println(name + "|Am adaugat un item");
	}
}
