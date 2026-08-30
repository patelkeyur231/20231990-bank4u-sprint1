namespace BankSprint1.Models
{
    /// <summary>
    /// A member of Bank Staff. Staff are still customers of the bank (they can hold
    /// accounts like anyone else) but receive a 50% discount on any transaction fees
    /// incurred, per the business rules. Both differences from a regular Customer are
    /// expressed purely through overriding two properties - no other class needs to
    /// know a discount is even happening.
    /// </summary>
    public class StaffCustomer : Customer
    {
        public StaffCustomer(string customerNumber, string name, string contactDetails)
            : base(customerNumber, name, contactDetails)
        {
        }

        public override decimal FeeDiscountRate
        {
            get { return 0.5m; }
        }

        public override string RoleLabel
        {
            get { return "Bank Staff (50% fee discount)"; }
        }
    }
}
