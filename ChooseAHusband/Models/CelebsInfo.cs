using System;
using System.Collections.Generic;

namespace ChooseAHusband.Models;

public partial class CelebsInfo
{
    public int CelebrityKod { get; set; }

    public int? CelebAge { get; set; }

    public string? CelebName { get; set; }

    public string? CelebHeight { get; set; }

    public string? CelebWeight { get; set; }

    public string? CelebDescription { get; set; }

    public string? CelebMeaningOfChoice { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<UserInfo> UserInfoUserFirstChoiceNavigations { get; } = new List<UserInfo>();

    public virtual ICollection<UserInfo> UserInfoUserLatestChoiceNavigations { get; } = new List<UserInfo>();
}
