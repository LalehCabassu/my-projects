// Fig. 12.12: Invoice.cs
// Invoice class implements IPayable.
using System;

public class Invoice : IPayable
{
   private int quantity;
   private decimal pricePerItem;

   // property that gets and sets the part number on the invoice
   public string PartNumber { get; set; }

   // property that gets and sets the part description on the invoice
   public string PartDescription { get; set; }

   // four-parameter constructor
   public Invoice( string part, string description, int count,
      decimal price )
   {
      PartNumber = part;
      PartDescription = description;
      Quantity = count; // validate quantity via property
      PricePerItem = price; // validate price per item via property
   } 

   // property that gets and sets the quantity on the invoice
   public int Quantity
   {
      get
      {
         return quantity;
      } 
      set
      {
         if ( value >= 0 ) // validate quantity
            quantity = value;
         else
            throw new ArgumentOutOfRangeException( "Quantity",
               value, "Quantity must be >= 0" );
      } 
   } 

   // property that gets and sets the price per item
   public decimal PricePerItem
   {
      get
      {
         return pricePerItem;
      } 
      set
      {
         if ( value >= 0 ) // validate price
            pricePerItem = value; 
         else
            throw new ArgumentOutOfRangeException( "PricePerItem",
               value, "PricePerItem must be >= 0" );
      } 
   } 

   // return string representation of Invoice object
   public override string ToString()
   {
      return string.Format(
         "{0}: \n{1}: {2} ({3}) \n{4}: {5} \n{6}: {7:C}",
         "invoice", "part number", PartNumber, PartDescription,
         "quantity", Quantity, "price per item", PricePerItem );
   } 

   // method required to carry out contract with interface IPayable
   public decimal GetPaymentAmount()
   {
      return Quantity * PricePerItem; // calculate total cost
   } 
} 