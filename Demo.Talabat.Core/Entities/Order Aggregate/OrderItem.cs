using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Talabat.Core.Entities.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        //will be tabel in database
        //the order-item is the product ordered as Item in the Order with specic price and specific quentity
        //so we need the order item to hold the product details  

        #region Related to Product  ---> encapsulate them in object as they are related to each other
        //we can say they are composite attribute for the order item 
        //put them in a new type -----> ProductItemOrdered

        /*public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!; */
        public ProductItemOrdered Product { get; set; }=null!;
        #endregion
        public decimal Price { get; set; } // the price of the product as a ITEM not the product price 
        /*el price as item mmkn ye5tlf depends on
         * the  date [fe el a3iad]
         * the quantity [kl ma teshtery aktr el price ynzel]
         * m3ak voucher 
         * hatedf3 visa bank cib 
         */
        public int Quantity { get; set; }


    }
}
