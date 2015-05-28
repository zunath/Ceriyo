﻿using System.ComponentModel;

namespace Ceriyo.Data.Enumerations
{
    public enum GameTypeEnum
    {
        [Description("Action")]
        Action = 1,
        [Description("Story")]
        Story = 2,
        [Description("Role Play")]
        RolePlay = 3,
        [Description("Team")]
        Team = 4,
        [Description("Melee")]
        Melee = 5,
        [Description("Social")]
        Social = 6,
        [Description("Alternative")]
        Alternative = 7,
        [Description("PW Action")]
        PWAction = 8,
        [Description("PW Story")]
        PWStory = 9,
        [Description("Solo")]
        Solo = 10
    }
}
