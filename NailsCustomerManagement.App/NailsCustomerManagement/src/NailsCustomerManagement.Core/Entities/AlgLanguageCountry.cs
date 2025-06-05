using System;
using System.Collections.Generic;

namespace NailsCustomerManagement.Core.Entities;

public partial class AlgLanguageCountry
{
    public int LanguageCountryId { get; set; }

    public string CountryNameEng { get; set; } = null!;

    public string CountryNameLatn { get; set; } = null!;

    public string CountryNameCyr { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public string DisplayLanguage { get; set; } = null!;

    public string ImgUrl { get; set; } = null!;

    public string LanguageSuffix { get; set; } = null!;
}
