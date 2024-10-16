//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.OrderLines = new HashSet<OrderLine>();
        }
    
        public long orderId { get; set; }
        public long creditCardId { get; set; }
        public long userId { get; set; }
        public System.DateTime orderDate { get; set; }
        public string postalAddressOrder { get; set; }
        public string orderName { get; set; }
        public decimal totalPrice { get; set; }
    
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_OrderCreditCardId
        /// </summary>
        public virtual CreditCard CreditCard { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_OrderLineOrderId
        /// </summary>
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_OrderUserId
        /// </summary>
        public virtual User User { get; set; }
    
    	/// <summary>
    	/// A hash code for this instance, suitable for use in hashing algorithms and data structures 
    	/// like a hash table. It uses the Josh Bloch implementation from "Effective Java"
        /// Primary key of entity is not included in the hash calculation to avoid errors
    	/// with Entity Framework creation of key values.
    	/// </summary>
    	/// <returns>
    	/// Returns a hash code for this instance.
    	/// </returns>
    	public override int GetHashCode()
    	{
    	    unchecked
    	    {
    			int multiplier = 31;
    			int hash = GetType().GetHashCode();
    
    			hash = hash * multiplier + creditCardId.GetHashCode();
    			hash = hash * multiplier + userId.GetHashCode();
    			hash = hash * multiplier + orderDate.GetHashCode();
    			hash = hash * multiplier + (postalAddressOrder == null ? 0 : postalAddressOrder.GetHashCode());
    			hash = hash * multiplier + (orderName == null ? 0 : orderName.GetHashCode());
    			hash = hash * multiplier + totalPrice.GetHashCode();
    
    			return hash;
    	    }
    
    	}
        
        /// <summary>
        /// Compare this object against another instance using a value approach (field-by-field) 
        /// </summary>
        /// <remarks>See http://www.loganfranken.com/blog/687/overriding-equals-in-c-part-1/ for detailed info </remarks>
    	public override bool Equals(object obj)
    	{
    
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type? 
    
            Order target = obj as Order;
    
    		return true
               &&  (this.orderId == target.orderId )       
               &&  (this.creditCardId == target.creditCardId )       
               &&  (this.userId == target.userId )       
               &&  (this.orderDate == target.orderDate )       
               &&  (this.postalAddressOrder == target.postalAddressOrder )       
               &&  (this.orderName == target.orderName )       
               &&  (this.totalPrice == target.totalPrice )       
               ;
    
        }
    
    
    	public static bool operator ==(Order  objA, Order  objB)
        {
            // Check if the objets are the same Order entity
            if(Object.ReferenceEquals(objA, objB))
                return true;
      
            return objA.Equals(objB);
    }
    
    
    	public static bool operator !=(Order  objA, Order  objB)
        {
            return !(objA == objB);
        }
    
    
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
    	public override String ToString()
    	{
    	    StringBuilder strOrder = new StringBuilder();
    
    		strOrder.Append("[ ");
           strOrder.Append(" orderId = " + orderId + " | " );       
           strOrder.Append(" creditCardId = " + creditCardId + " | " );       
           strOrder.Append(" userId = " + userId + " | " );       
           strOrder.Append(" orderDate = " + orderDate + " | " );       
           strOrder.Append(" postalAddressOrder = " + postalAddressOrder + " | " );       
           strOrder.Append(" orderName = " + orderName + " | " );       
           strOrder.Append(" totalPrice = " + totalPrice + " | " );       
            strOrder.Append("] ");    
    
    		return strOrder.ToString();
        }
    
    
    }
}
