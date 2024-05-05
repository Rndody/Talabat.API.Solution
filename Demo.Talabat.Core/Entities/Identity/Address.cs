namespace Demo.Talabat.Core.Entities.Identity
{
    public class Address : BaseEntity //the base entity class is made for all entities so we can use it here also 
    {
        #region Properties
        // public int Id { get; set; } // we inherited this property from the  BaseEntity class
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;

        #region Foreign key
        public string ApplicationUserId { get; set; }
        #endregion

        #region navigational property
        ///navigational property[0ne]
        public ApplicationUser User { get; set; } = null!;
        #endregion

        #endregion

    }
}