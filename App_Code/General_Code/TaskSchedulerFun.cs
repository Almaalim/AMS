using System;
using TaskScheduler;

public class TaskSchedulerFun
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected string Start_TaskName = "";
    protected string Stop_TaskName  = "";  
    protected string ServiceName    = "";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public TaskSchedulerFun() { }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void CreateScheduled(string ImportTimes, string ProcessTimes, bool isRunProcess)
    {
        GetTranaction_DeleteScheduled();
        RunProcess_DeleteScheduled();

        GetTranaction_CreateScheduled(ImportTimes);
        if (!isRunProcess ) {RunProcess_CreateScheduled(ProcessTimes); }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void GetTranaction_CreateScheduled(string Times)
    {
        ScheduledTasks st = new ScheduledTasks();

        Start_TaskName = "WF_GetTranaction_START";
        Stop_TaskName  = "WF_GetTranaction_STOP";      
        ServiceName    = "WF_GetTransaction";

        DeleteScheduled();
        CreateTask(Times);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void GetTranaction_DeleteScheduled()
    {
        Start_TaskName = "WF_GetTranaction_START";
        Stop_TaskName  = "WF_GetTranaction_STOP";  
        DeleteScheduled();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void RunProcess_CreateScheduled(string Times)
    {
        Start_TaskName = "WF_RunProcess_START";
        Stop_TaskName  = "WF_RunProcess_STOP";      
        ServiceName    = "WF_RunProcess";

        DeleteScheduled();
        CreateTask(Times);
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void RunProcess_DeleteScheduled()
    {
        Start_TaskName = "WF_RunProcess_START";
        Stop_TaskName  = "WF_RunProcess_STOP";  
        DeleteScheduled();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void DeleteScheduled()
    {
        ScheduledTasks st = new ScheduledTasks();

        st.DeleteTask(Start_TaskName);
        st.DeleteTask(Stop_TaskName);
       
        st.Dispose();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void CreateTask(string Times)
    {
        ScheduledTasks st = new ScheduledTasks();

        Task tStart;
        Task tStop;

        try { tStart = st.CreateTask(Start_TaskName); } catch (ArgumentException) { Console.WriteLine("Task name already exists"); return; }
        try { tStop  = st.CreateTask(Stop_TaskName);  } catch (ArgumentException) { Console.WriteLine("Task name already exists"); return; }

        tStart.ApplicationName = "NET";
		tStart.Parameters = "START " + ServiceName + "";
		tStart.Comment = "START WF GetTranaction Service";

        tStop.ApplicationName = "NET";
		tStop.Parameters = "STOP " + ServiceName + "";
		tStop.Comment = "STOP WF GetTranaction Service";

        tStart.SetAccountInformation(@"", (string)null);
        tStop.SetAccountInformation(@"", (string)null);
        
        tStart.IdleWaitMinutes = 10;// Declare that the system must have been idle for ten minutes before the task will start
		tStart.MaxRunTime = new TimeSpan(2, 30, 0); // Allow the task to run for no more than 2 hours, 30 minutes.	
        tStart.Priority = System.Diagnostics.ProcessPriorityClass.Idle; // Set priority to only run when system is idle.	


		tStop.IdleWaitMinutes = 10;// Declare that the system must have been idle for ten minutes before the task will start
		tStop.MaxRunTime = new TimeSpan(2, 30, 0); // Allow the task to run for no more than 2 hours, 30 minutes.	
        tStop.Priority = System.Diagnostics.ProcessPriorityClass.Idle; // Set priority to only run when system is idle.
			
        if (!string.IsNullOrEmpty(Times))
        {
            string[] Time = Times.Split(',');
            foreach(string tm in Time)
            {
                string[] part = tm.Split(':');
                short H = Convert.ToInt16(part[0]);
                short M = Convert.ToInt16(part[1]);

                if (M == 0) { tStop.Triggers.Add(new DailyTrigger(Convert.ToInt16(H - 1), 59)); } else { tStop.Triggers.Add(new DailyTrigger(H, Convert.ToInt16(M - 1))); }
                tStart.Triggers.Add(new DailyTrigger(H, M));
            }
        }

        // Save and close.
        tStart.Save();
        tStop.Save();
        tStart.Close();
		tStop.Close();     
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void GetListTasks()
    {
        ScheduledTasks st = new ScheduledTasks();
		string[] taskNames = st.GetTaskNames();
		// Open each task, dump info to console
		foreach (string name in taskNames)
        {
			Task t = st.OpenTask(name);
			t.Close();
		}
	}
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}