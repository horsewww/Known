﻿namespace Known.AntBlazor;

public class QueryDataForm : Form<Dictionary<string, QueryInfo>> { }

public class LoginInfoForm : Form<LoginFormInfo>
{
    public LoginInfoForm()
    {
        LabelCol = null;
        ValidateMode = FormValidateMode.Rules;
    }
}