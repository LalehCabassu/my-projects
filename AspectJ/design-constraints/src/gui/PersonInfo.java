package gui;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.eclipse.swt.SWT;
import org.eclipse.swt.events.SelectionAdapter;
import org.eclipse.swt.events.SelectionEvent;
import org.eclipse.swt.events.SelectionListener;
import org.eclipse.swt.layout.GridLayout;
import org.eclipse.swt.widgets.Button;
import org.eclipse.swt.widgets.Combo;
import org.eclipse.swt.widgets.Display;
import org.eclipse.swt.widgets.Group;
import org.eclipse.swt.widgets.Label;
import org.eclipse.swt.widgets.Shell;
import org.eclipse.swt.widgets.Text;
import org.eclipse.wb.swt.SWTResourceManager;

import personalHealthInfo.Address;
import personalHealthInfo.GenerateObjects;
import personalHealthInfo.Name;
import personalHealthInfo.Patient;
import personalHealthInfo.Person;
import personalHealthInfo.PhoneNumber;
import org.eclipse.swt.widgets.Menu;
import org.eclipse.swt.widgets.MenuItem;
import org.eclipse.swt.layout.GridData;
import org.eclipse.swt.widgets.Table;
import org.eclipse.swt.widgets.TableColumn;
import org.eclipse.swt.events.ShellAdapter;
import org.eclipse.swt.events.ShellEvent;

public class PersonInfo {

protected Shell shlPersoninfoui;
	
	public static List<Person> persons = new ArrayList<Person>();
	private int selectedIndex;		
	private boolean editMode = true;     //true: edit, false: add
	
	private org.eclipse.swt.widgets.List personsList;
	
	private Combo nameType;
	private Text salutation;
	private Text firstName;
	private Text lastName;
	private Text suffix;
	
	private Combo addressType;
	private Text streetLine1;
	private Text streetLine2;
	private Text city;
	private Text state;
	private Text postalCode;
	
	private Combo phoneNumberType;
	private Text areaCode;
	private Text exchange;
	private Text detailNumber;
	private Text extension;
	
	private Text middleName;
	
	/**
	 * Launch the application.
	 * @param args
	 */
	public static void main(String[] args)  throws IOException {
		try {
			PersonInfo window = new PersonInfo();
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
		shlPersoninfoui.open();
		shlPersoninfoui.layout();
		while (!shlPersoninfoui.isDisposed()) {
			if (!display.readAndDispatch()) {
				display.sleep();
			}
		}
	}
	
	/**
	 * Create contents of the window.
	 */
	protected void createContents() {
		shlPersoninfoui = new Shell();
		shlPersoninfoui.setLocation(0, 0);
		shlPersoninfoui.setSize(404, 753);
		shlPersoninfoui.setText("Person Information");
		shlPersoninfoui.setLayout(new GridLayout(1, false));
		
		Button btnNewPerson = new Button(shlPersoninfoui, SWT.NONE);
		btnNewPerson.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				clearForm();
				editMode = false;
			}
		});
		btnNewPerson.setLayoutData(new GridData(SWT.CENTER, SWT.CENTER, false, false, 1, 1));
		btnNewPerson.setText("New Person");
		
		personsList = new org.eclipse.swt.widgets.List(shlPersoninfoui, SWT.BORDER);
		GridData gd_personsList = new GridData(SWT.LEFT, SWT.CENTER, false, false, 1, 1);
		gd_personsList.heightHint = 156;
		gd_personsList.widthHint = 367;
		personsList.setLayoutData(gd_personsList);
		personsList.addSelectionListener(new SelectionListener() {
		      public void widgetSelected(SelectionEvent event) {
		        int[] selectedItems = personsList.getSelectionIndices();
		        selectedIndex = selectedItems[0];
		        populatePerson(persons.get(selectedIndex));
		      }

		      public void widgetDefaultSelected(SelectionEvent event) {
		    	  selectedIndex = 0;
		    	  populatePerson(persons.get(selectedIndex));
		      }
		    });
		loadPersons();
		
		Group grpName = new Group(shlPersoninfoui, SWT.NONE);
		grpName.setFont(SWTResourceManager.getFont("Segoe UI", 9, SWT.BOLD | SWT.ITALIC));
		grpName.setText("Name");
		
		Label lblSalutation = new Label(grpName, SWT.NONE);
		lblSalutation.setBounds(10, 54, 55, 15);
		lblSalutation.setText("Salutation:");
		
		Label lblFirstName = new Label(grpName, SWT.NONE);
		lblFirstName.setBounds(10, 78, 68, 15);
		lblFirstName.setText("First Name:");
		
		Label lblLastName = new Label(grpName, SWT.NONE);
		lblLastName.setBounds(10, 126, 68, 15);
		lblLastName.setText("Last Name:");
		
		Label lblSuffix = new Label(grpName, SWT.NONE);
		lblSuffix.setBounds(10, 150, 55, 15);
		lblSuffix.setText("Suffix:");
		
		salutation = new Text(grpName, SWT.BORDER);
		salutation.setBounds(91, 51, 76, 21);
		
		firstName = new Text(grpName, SWT.BORDER);
		firstName.setBounds(91, 75, 150, 21);
		
		lastName = new Text(grpName, SWT.BORDER);
		lastName.setBounds(91, 123, 150, 21);
		
		suffix = new Text(grpName, SWT.BORDER);
		suffix.setBounds(91, 147, 76, 21);
		
		Label lblNameType = new Label(grpName, SWT.NONE);
		lblNameType.setText("Type:");
		lblNameType.setBounds(10, 27, 55, 15);
		
		nameType = new Combo(grpName, SWT.NONE);
		nameType.setItems(new String[] {"LEGAL", "COMMON", "ALIAS"});
		nameType.setBounds(91, 24, 91, 23);
		nameType.select(0);
		
		Label lblMiddleName = new Label(grpName, SWT.NONE);
		lblMiddleName.setText("Middle Name:");
		lblMiddleName.setBounds(10, 102, 76, 15);
		
		middleName = new Text(grpName, SWT.BORDER);
		middleName.setBounds(91, 99, 150, 21);
		
		Group grpPhoneNumber = new Group(shlPersoninfoui, SWT.NONE);
		grpPhoneNumber.setFont(SWTResourceManager.getFont("Segoe UI", 9, SWT.BOLD | SWT.ITALIC));
		grpPhoneNumber.setText("Phone Number");
		
		Label lblPhoneNumber = new Label(grpPhoneNumber, SWT.NONE);
		lblPhoneNumber.setText("Phone Number:");
		lblPhoneNumber.setBounds(10, 49, 91, 15);
		
		areaCode = new Text(grpPhoneNumber, SWT.BORDER);
		areaCode.setBounds(117, 46, 39, 21);
		
		Label lblPhoneNumberType = new Label(grpPhoneNumber, SWT.NONE);
		lblPhoneNumberType.setText("Type:");
		lblPhoneNumberType.setBounds(10, 25, 55, 15);
		
		phoneNumberType = new Combo(grpPhoneNumber, SWT.NONE);
		phoneNumberType.setItems(new String[] {"HOME", "CELL_PHONE", "WORK", "FAX", "OTHER"});
		phoneNumberType.setBounds(107, 22, 110, 23);
		phoneNumberType.select(0);
		
		Label lblPostPeranthesis = new Label(grpPhoneNumber, SWT.NONE);
		lblPostPeranthesis.setBounds(162, 49, 15, 15);
		lblPostPeranthesis.setText(") -");
		
		exchange = new Text(grpPhoneNumber, SWT.BORDER);
		exchange.setBounds(178, 46, 39, 21);
		
		Label lblPreParanthesis = new Label(grpPhoneNumber, SWT.NONE);
		lblPreParanthesis.setText("(");
		lblPreParanthesis.setBounds(106, 49, 5, 15);
		
		Label lblDash = new Label(grpPhoneNumber, SWT.NONE);
		lblDash.setText("-");
		lblDash.setBounds(223, 49, 5, 15);
		
		detailNumber = new Text(grpPhoneNumber, SWT.BORDER);
		detailNumber.setBounds(234, 46, 76, 21);
		
		Label lblDash2 = new Label(grpPhoneNumber, SWT.NONE);
		lblDash2.setText("-");
		lblDash2.setBounds(316, 49, 5, 15);
		
		extension = new Text(grpPhoneNumber, SWT.BORDER);
		extension.setBounds(327, 46, 39, 21);
		
		Group grpAddress = new Group(shlPersoninfoui, SWT.NONE);
		grpAddress.setFont(SWTResourceManager.getFont("Segoe UI", 9, SWT.BOLD | SWT.ITALIC));
		grpAddress.setText("Address");
		
		Label lblStreetLine1 = new Label(grpAddress, SWT.NONE);
		lblStreetLine1.setBounds(10, 49, 75, 15);
		lblStreetLine1.setText("Street Line 1:");
		
		Label lblStreetLine2 = new Label(grpAddress, SWT.NONE);
		lblStreetLine2.setBounds(10, 73, 75, 15);
		lblStreetLine2.setText("Street Line 2:");
		
		Label lblCity = new Label(grpAddress, SWT.NONE);
		lblCity.setBounds(10, 98, 55, 15);
		lblCity.setText("City:");
		
		Label lblCountry = new Label(grpAddress, SWT.NONE);
		lblCountry.setBounds(10, 122, 55, 15);
		lblCountry.setText("State:");
		
		Label lblPostalCode = new Label(grpAddress, SWT.NONE);
		lblPostalCode.setBounds(10, 146, 75, 15);
		lblPostalCode.setText("Postal code:");
		
		streetLine1 = new Text(grpAddress, SWT.BORDER);
		streetLine1.setBounds(91, 46, 268, 21);
		
		streetLine2 = new Text(grpAddress, SWT.BORDER);
		streetLine2.setBounds(91, 70, 268, 21);
		
		city = new Text(grpAddress, SWT.BORDER);
		city.setBounds(91, 95, 107, 21);
		
		state = new Text(grpAddress, SWT.BORDER);
		state.setBounds(91, 119, 107, 21);
		
		postalCode = new Text(grpAddress, SWT.BORDER);
		postalCode.setBounds(91, 143, 107, 21);
		
		Label lblAddressType = new Label(grpAddress, SWT.NONE);
		lblAddressType.setBounds(10, 25, 55, 15);
		lblAddressType.setText("Type:");
		
		addressType = new Combo(grpAddress, SWT.NONE);
		addressType.setItems(new String[] {"HOME", "WORK", "OTHER"});
		addressType.setBounds(91, 22, 91, 23);
		addressType.select(0);
		
		Button btnSave = new Button(shlPersoninfoui, SWT.NONE);
		GridData gd_btnSave = new GridData(SWT.LEFT, SWT.CENTER, false, false, 1, 1);
		gd_btnSave.widthHint = 62;
		btnSave.setLayoutData(gd_btnSave);
		btnSave.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				
				Name.NameTypeEnum nt;
				String selectedNT = nameType.getText(); 
				if(selectedNT.equals("COMMON"))
					nt = Name.NameTypeEnum.COMMON;
				else if(selectedNT.equals("ALIAS"))
					nt = Name.NameTypeEnum.ALIAS;
				else
					nt = Name.NameTypeEnum.LEGAL;
				
				Address.AddressTypeEnum at;
				String selectedAT = addressType.getText();
				if(selectedAT.equals("HOME"))
					at = Address.AddressTypeEnum.HOME;
				else if(selectedAT.equals("WORK"))
					at = Address.AddressTypeEnum.WORK;
				else
					at = Address.AddressTypeEnum.OTHER;
				
				PhoneNumber.PhoneTypeEnum pt;
				String selectedPT = phoneNumberType.getText();
				if(selectedPT.equals("HOME"))
					pt = PhoneNumber.PhoneTypeEnum.HOME;
				else if(selectedPT.equals("CELL_PHONE"))
					pt = PhoneNumber.PhoneTypeEnum.CELL_PHONE;
				else if(selectedPT.equals("WORK"))
					pt = PhoneNumber.PhoneTypeEnum.WORK;
				else if(selectedPT.equals("FAX"))
					pt = PhoneNumber.PhoneTypeEnum.FAX;
				else
					pt = PhoneNumber.PhoneTypeEnum.OTHER;
				
				if(editMode)
				{
					Person person = persons.get(selectedIndex);
					Name name = (person.getNames().size() > 0) ? person.getNames().get(0) : null;
					PhoneNumber phone = (person.getPhoneNumbers().size() > 0) ? person.getPhoneNumbers().get(0) : null;
					Address address = (person.getAddresses().size() > 0) ? person.getAddresses().get(0) : null;
					
					name.SetSalutation(salutation.getText());
					name.SetNameType(nt);
					name.SetFirstName(firstName.getText());
					name.SetMiddleName(middleName.getText());;
					name.SetLastName(lastName.getText());
					name.SetSuffix(suffix.getText());
					
					phone.SetPhoneType(pt);
					phone.SetAreaCode(areaCode.getText());
					phone.SetExchange(exchange.getText());
					phone.SetDetailNumber(detailNumber.getText());
					phone.SetExtension(extension.getText());
					
					address.SetAddressType(at);
					address.SetStreesLine1(streetLine1.getText());
					address.SetStreetLine2(streetLine2.getText());
					address.SetCity(city.getText());
					address.SetState(state.getText());
					address.SetPostalCode(postalCode.getText());
				}
				else
				{
					Name newName = new Name(nt, salutation.getText(), firstName.getText(), 
							middleName.getText(), lastName.getText(), suffix.getText());
					Address newAddress = new Address(at, streetLine1.getText(), streetLine2.getText(), 
												city.getText(), state.getText(), postalCode.getText());
					PhoneNumber newPhoneNumber = new PhoneNumber(pt, areaCode.getText(), exchange.getText(), 
														detailNumber.getText(), extension.getText());
					
					Person newPerson = new Person(newName, newPhoneNumber, newAddress);
					persons.add(newPerson);
				}
				loadPersons();
			}
		});
		btnSave.setText("Save");
		
		Button btnDelete = new Button(shlPersoninfoui, SWT.NONE);
		btnDelete.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				if(editMode)
				{
					persons.remove(selectedIndex);
					loadPersons();
				}
			}
		});
		GridData gd_btnDelete = new GridData(SWT.CENTER, SWT.CENTER, false, false, 1, 1);
		gd_btnDelete.widthHint = 63;
		btnDelete.setLayoutData(gd_btnDelete);
		btnDelete.setText("Delete");
		
		Button btnCancel = new Button(shlPersoninfoui, SWT.NONE);
		btnCancel.addSelectionListener(new SelectionAdapter() {
			@Override
			public void widgetSelected(SelectionEvent e) {
				if(editMode)
					populatePerson(persons.get(selectedIndex));
				else
					clearForm();
			}
		});
		GridData gd_btnCancel = new GridData(SWT.RIGHT, SWT.CENTER, false, false, 1, 1);
		gd_btnCancel.widthHint = 66;
		btnCancel.setLayoutData(gd_btnCancel);
		btnCancel.setText("Cancel");

	}
	
	private void loadPersons() {
		if(persons.size() == 0)
			persons = GenerateObjects.generatePersons(10);
		personsList.removeAll();
		for(Person p: persons)
		{
			List<Name> names = p.getNames();
			String fullName = (names.size() > 0) ? names.get(0).GetSortName() : "";
			if(!fullName.isEmpty())
				personsList.add(fullName);
		}
	}
	
	private void populatePerson(Person person)
	{
		Name name = (person.getNames().size() > 0) ? person.getNames().get(0) : null;
		PhoneNumber phone = (person.getPhoneNumbers().size() > 0) ? person.getPhoneNumbers().get(0) : null;
		Address address = (person.getAddresses().size() > 0) ? person.getAddresses().get(0) : null;
		
		salutation.setText(name.GetSalutation());
		nameType.setText(name.GetNameType().toString());
		firstName.setText(name.GetFirstName());
		middleName.setText(name.GetMiddleName());
		lastName.setText(name.GetLastName());
		suffix.setText(name.GetSuffix());
		
		this.phoneNumberType.setText(phone.GetPhoneType().toString());
		this.areaCode.setText(phone.GetAreaCode());
		this.exchange.setText(phone.GetExchange());
		this.detailNumber.setText(phone.GetDetailNumber());
		this.extension.setText(phone.GetExtension());
		
		this.addressType.setText(address.GetAddressType().toString());
		this.streetLine1.setText(address.GetStreetLine1());
		this.streetLine2.setText(address.GetStreetLine2());
		this.city.setText(address.GetCity());
		this.state.setText(address.GetState());
		this.postalCode.setText(address.GetPostalCode());
	}
	
	private void clearForm()
	{
		salutation.setText("");
		nameType.select(0);
		firstName.setText("");
		middleName.setText("");
		lastName.setText("");
		suffix.setText("");
		
		this.phoneNumberType.select(0);
		this.areaCode.setText("");
		this.exchange.setText("");
		this.detailNumber.setText("");
		this.extension.setText("");
		
		this.addressType.select(0);
		this.streetLine1.setText("");
		this.streetLine2.setText("");
		this.city.setText("");
		this.state.setText("");
		this.postalCode.setText("");
	}
}
