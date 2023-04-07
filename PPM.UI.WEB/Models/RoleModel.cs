namespace PPM.UI.WEB.Models;

public class RoleModel
{
    public int roleId {get; set;} 
    public string roleName {get; set;} 

    public RoleModel(int roleId, string roleName)
    {
        this.roleId = roleId;
        this.roleName = roleName;   
    }

    public RoleModel ()
    {
            
    } 
}