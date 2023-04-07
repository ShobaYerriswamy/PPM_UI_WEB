namespace PPM.UI.WEB.Models;

public class ProjectModel
{
    public string projectName {get; set;}
    public string startDate {get; set;} 
    public string endDate {get; set;} 
    public int id {get; set;} 

    public ProjectModel (string projectname, string startdate, string enddate, int Id )
    {
        this.projectName = projectname;
        this.startDate = startdate;
        this.endDate = enddate;
        this.id = Id;
    }

    public ProjectModel ()
    {

    }

}