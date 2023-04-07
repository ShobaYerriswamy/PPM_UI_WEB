namespace PPM.UI.WEB.Models;

public class EmployeeModel
{
    public int employeeID {get; set;}

    public string employeefirstName{get;set;}
    public string lastName {get; set;} 
    public string email {get; set;} 
    public string mobile {get; set;} 
    public string address {get; set;} 
    public int roleId {get; set;} 
     public string password {get; set;} 
       
       

    public EmployeeModel(int employeeid, string FirstName, string LastName, string Email, string Mobile, string Address, int RoleID, string Password)
    {
        this.employeeID = employeeid;  
        this.employeefirstName = FirstName;
        this.lastName = LastName;
        this.email = Email;
        this.mobile = Mobile;
        this.address = Address;
        this.roleId = RoleID;
        this.password = Password; 
           
    }

    public EmployeeModel ()
    {

    }

}
