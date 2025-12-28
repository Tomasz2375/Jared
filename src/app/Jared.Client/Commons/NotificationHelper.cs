namespace Jared.Client.Commons;

public static class NotificationHelper
{
    #region Task
    public static string TaskCreated(string taskTitle)
        => $"Task '{taskTitle}' successfully created.";
    public static string TaskCreationFailed(string message)
        => $"Failed to create task. '{message}'";
    public static string TaskUpdated(string taskTitle)
        => $"Task '{taskTitle}' successfully updated.";
    public static string TaskUpdateFailed(string message)
        => $"Failed to update task. '{message}'";
    public static string TaskFetchFailed(string message)
        => $"Failed to load task '{message}'.";
    public static string TasksFetchFailed(string message)
        => $"Failed to load tasks. '{message}'";
    #endregion

    #region Project
    public static string ProjectCreated(string projectTitle)
        => $"Project '{projectTitle}' successfully created.";
    public static string ProjectCreationFailed(string message)
        => $"Failed to create project. '{message}'";
    public static string ProjectUpdated(string projectTitle)
        => $"Project '{projectTitle}' successfully updated.";
    public static string ProjectUpdateFailed(string message)
        => $"Failed to update project. '{message}'";
    public static string ProjectFetchFailed(string message)
        => $"Failed to load project '{message}'.";
    public static string ProjectsFetchFailed(string message)
        => $"Failed to load projects. '{message}'";
    #endregion

    #region Users
    public static string UsersFetchFailed(string message)
        => $"Failed to load users. '{message}'";
    #endregion
}
