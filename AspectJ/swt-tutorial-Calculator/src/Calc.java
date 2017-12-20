import org.eclipse.jface.dialogs.MessageDialog;
import org.eclipse.swt.widgets.Display;
import org.eclipse.swt.widgets.Shell;
import org.eclipse.swt.widgets.Button;
import org.eclipse.swt.SWT;
import org.eclipse.swt.widgets.Text;
import org.eclipse.swt.widgets.Label;
import org.eclipse.swt.events.SelectionAdapter;
import org.eclipse.swt.events.SelectionEvent;
import org.eclipse.wb.swt.SWTResourceManager;


public class Calc {

	protected Shell shlCalculator;
	private Text firstNumber;
	private Text secondNumber;
	private Button subtractButton;
	private Button addButton;
	private Label resultLabel;

	/**
	 * Launch the application.
	 * @param args
	 */
	public static void main(String[] args) {
		try {
			Calc window = new Calc();
			window.open();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * Open the window.
	 */
	public void open() {
		Display display = Display.getDefault();
		createContents();
		shlCalculator.open();
		shlCalculator.layout();
		while (!shlCalculator.isDisposed()) {
			if (!display.readAndDispatch()) {
				display.sleep();
			}
		}
	}

	/**
	 * Create contents of the window.
	 */
	protected void createContents() {
		shlCalculator = new Shell();
		shlCalculator.setSize(203, 258);
		shlCalculator.setText("Calculator");
		
		firstNumber = new Text(shlCalculator, SWT.BORDER);
		firstNumber.setBounds(10, 31, 138, 21);
		
		secondNumber = new Text(shlCalculator, SWT.BORDER);
		secondNumber.setBounds(10, 81, 138, 21);
		
		Label firstNumberLabel = new Label(shlCalculator, SWT.NONE);
		firstNumberLabel.setBounds(10, 10, 100, 15);
		firstNumberLabel.setText("First number");
		
		Label secondNumberLabel = new Label(shlCalculator, SWT.NONE);
		secondNumberLabel.setBounds(10, 60, 100, 15);
		secondNumberLabel.setText("Second number");
		
		subtractButton = new Button(shlCalculator, SWT.NONE);
		subtractButton.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				try
				{
					int number1 = Integer.parseInt(firstNumber.getText());
					int number2 = Integer.parseInt(secondNumber.getText());
					resultLabel.setText("The result is " + (number1 - number2));
				}
				catch(Exception exc)
				{
					MessageDialog.openError(shlCalculator, "Error", "Bad number.\n" + exc.getMessage());
					resultLabel.setText("The result is ...");
					return;
				}
			}
		});
		subtractButton.setToolTipText("Substract");
		subtractButton.setBounds(83, 124, 65, 25);
		subtractButton.setText("-");
		
		addButton = new Button(shlCalculator, SWT.NONE);
		addButton.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				try
				{
					int number1 = Integer.parseInt(firstNumber.getText());
					int number2 = Integer.parseInt(secondNumber.getText());
					resultLabel.setText("The result is " + (number1 + number2));
				}
				catch(Exception exc)
				{
					MessageDialog.openError(shlCalculator, "Error", "Bad number.\n" + exc.getMessage());
					resultLabel.setText("The result is ...");
					return;
				}
			}
		});
		addButton.setToolTipText("Add");
		addButton.setBounds(12, 124, 65, 25);
		addButton.setText("+");
		
		resultLabel = new Label(shlCalculator, SWT.NONE);
		resultLabel.setFont(SWTResourceManager.getFont("Segoe UI", 12, SWT.BOLD | SWT.ITALIC));
		resultLabel.setBounds(10, 170, 138, 40);
		resultLabel.setText("The result is ...");
	}
}
