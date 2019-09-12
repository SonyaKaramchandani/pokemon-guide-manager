
public class Rootobject
{
    public GeoNamesClass[] Property1 { get; set; }
}

public class GeoNamesClass
{
    public string name { get; set; }
    public string countryCode { get; set; }
    public string featureCodeDesc { get; set; }
    public int geonameId { get; set; }
    public string alternatenames { get; set; }
    public float latitude { get; set; }
    public string asciiname { get; set; }
    public float longitude { get; set; }
    public int? elevation { get; set; }
    public string lastModified { get; set; }
    public object admin3Code { get; set; }
    public Country[] country { get; set; }
    public Alternatenamesinfo[] alternateNamesInfo { get; set; }
    public string featureClass { get; set; }
    public object cc2 { get; set; }
    public string timezone { get; set; }
    public string admin1Code { get; set; }
    public long population { get; set; }
    public string classCode { get; set; }
    public string featureCodeName { get; set; }
    public string featureCode { get; set; }
    public object admin4Code { get; set; }
    public object admin2Code { get; set; }
    public Admin1[] admin1 { get; set; }
    public int dem { get; set; }
}

public class Country
{
    public string ISO { get; set; }
    public string FIPS { get; set; }
    public int ISOnumeric { get; set; }
    public string countryName { get; set; }
    public int geonameId { get; set; }
    public string ISO3 { get; set; }
}

public class Alternatenamesinfo
{
    public bool isShortName { get; set; }
    public string alternateName { get; set; }
    public bool isPreferredName { get; set; }
    public bool isHistoric { get; set; }
    public string isoLanguage { get; set; }
    public bool isColloquial { get; set; }
}

public class Admin1
{
    public int geonameId { get; set; }
    public string asciiName { get; set; }
    public string ucs2Name { get; set; }
    public string code { get; set; }
}
