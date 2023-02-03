using System;
using System.Collections.Generic;

namespace ChooseAHusband.Models;

public partial class UserInfo
{
    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string? UserName { get; set; }

    public int? UserFirstChoice { get; set; }

    public int? UserLatestChoice { get; set; }

    public string? UserTimesOnApp { get; set; }

    public int UserKod { get; set; }

    public virtual CelebsInfo? UserFirstChoiceNavigation { get; set; }

    public virtual CelebsInfo? UserLatestChoiceNavigation { get; set; }
}
