namespace BCP.ApiModels.Report;

public class ReportResponseModel
{
    public string BikeName { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string User { get; set; }
    public bool IsReturned { get; set; }
}