using System;

namespace Service.DatabaseConnection.Models
{
  public class BestenlisteModel
  {
    public int? WfID { get; set; }
    public int? WettkampfID { get; set; }
    public int? FahrerID { get; set; }

    //Hässlich aber egal
    public string FahrerName { get => Fahrer.Fullname; }

    public int? Fahrer_startnummer { get; set; }
    public TimeSpan Zeit { get; set; }



   

    public FahrerModel Fahrer { get; set; }
    //public WettkampfModel wettkampf { get; set; }



  }
}
