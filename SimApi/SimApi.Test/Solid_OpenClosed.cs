namespace SimApi.Test;

public class ReportDefinition
{
    public string ReportType { get; set; }


    public void Generate(Person person)
    {
        if (ReportType == "pdf" )
        {

        }
        if (ReportType == "cvs")
        {

        }
    }
}

public abstract class ReportDefinitionNew
{
    public string ReportName { get; set; }

    public abstract void Generate(Person person);
}

public class PdfReport : ReportDefinitionNew
{
    public override void Generate(Person person)
    {
        throw new NotImplementedException();
    }
}

public class CsvReport : ReportDefinitionNew
{
    public override void Generate(Person person)
    {
        throw new NotImplementedException();
    }
}