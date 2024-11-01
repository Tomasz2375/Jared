namespace Jared.Presentation.Requests;

public static class BaseAdresses
{
    #region Tasks
    public const string TASK_CREATE = "task/create";
    public const string TASK_UPDATE = "task/update";
    public const string TASK_DETAILS = "task";
    public const string TASK_LIST = "task/list";
    public const string TASK_PAGE = "task/page";
    #endregion

    #region Epics
    public const string EPIC_CREATE = "epic/create";
    public const string EPIC_UPDATE = "epic/update";
    public const string EPIC_DETAILS = "epic";
    public const string EPIC_LIST = "epic/list";
    public const string EPIC_PAGE = "epic/page";
    #endregion

    #region Projects
    public const string PROJECT_CREATE = "project/create";
    public const string PROJECT_UPDATE = "project/update";
    public const string PROJECT_DETAILS = "project";
    public const string PROJECT_LIST = "project/list";
    public const string PROJECT_PAGE = "project/page";
    #endregion

    #region Users
    public const string USER_LIST = "user/list";
    public const string USER_LOGIN = "user/login";
    public const string USER_REGISTER = "user/register";
    public const string USER_PASSWORD = "user/password";
    public const string USER_UPDATE = "user/update";
    #endregion

    #region Roles
    public const string ROLE_LIST = "role/list";
    #endregion
}
