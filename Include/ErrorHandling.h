#define CHECK_TEAM_PROJECT(tp) if(-not tp) { throw "No TFS team project information available. Either supply a valid -Project argument or use Connect-TfsTeamProject prior to invoking this cmdlet." }
#define CHECK_ASYNC(TASK,RESULT,MESSAGE) RESULT = TASK.Result; if(TASK.IsFaulted) { _throw MESSAGE TASK.Exception.InnerExceptions }
#define CALL_ASYNC(METHOD,TASK,RESULT,ERROR_MSG) TASK = METHOD; CHECK_ASYNC(TASK,RESULT,ERROR_MSG)
#define CALL_ASYNC(METHOD,ERROR_MSG) $task = METHOD; CHECK_ASYNC($task,$result,ERROR_MSG)