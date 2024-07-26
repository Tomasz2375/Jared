namespace Jared.Presentation.CQRS;

public static class BaseAdresses
{
    #region Tasks
    public const string TASK_CREATE = "Task/Create";
    public const string TASK_UPDATE = "Task/Update";
    public const string TASK_DETAILS = "Task";
    public const string TASK_LIST = "Task/List";
    public const string TASK_PAGE = "Task/Page";
    #endregion

    #region Epics
    public const string EPIC_CREATE = "Epic/Create";
    public const string EPIC_UPDATE = "Epic/Update";
    public const string EPIC_DETAILS = "Epic";
    public const string EPIC_LIST = "Epic/List";
    public const string EPIC_PAGE = "Epic/Page";
    #endregion

    #region Projects
    public const string PROJECT_CREATE = "Project/Create";
    public const string PROJECT_UPDATE = "Project/Update";
    public const string PROJECT_DETAILS = "Project";
    public const string PROJECT_LIST = "Project/List";
    public const string PROJECT_PAGE = "Project/Page";
    #endregion

    #region Authorization
    public const string USER_LOGIN = "User/Login";
    public const string USER_REGISTER = "User/Register";
    #endregion
}
