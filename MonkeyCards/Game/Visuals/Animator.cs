namespace Game.Visuals;

public class Animator
{
    private class AnimationTask
    {
        public Action<float> Action;
        public float Progress;
        public float Duration;
        public float Delay;
        public bool Removable;
        public bool IsCompleted;
        public bool Mirror;
        public bool IsPlayingForward = true;
    }
    
    private readonly List<AnimationTask> _tasks = new();
    
    ~Animator() => _tasks.Clear();
    
    // TODO: add OnComplete event in end animation
    public void Task(Action<float> action, float duration = 1f, float delay = 0f, bool removable = false, bool mirror = false)
    {
        _tasks.Add(new AnimationTask
        {
            Action = action,
            Progress = 0f,
            Duration = duration,
            Delay = delay,
            Removable = removable,
            IsCompleted = false,
            Mirror = mirror,
            IsPlayingForward = true
        });
    }
    
    // required call in update
    public void Update(float deltaTime)
    {
        for (int i = _tasks.Count - 1; i >= 0; i--)
        {
            var task = _tasks[i];

            if (task.Delay > 0f)
            {
                task.Delay -= deltaTime;
                continue;
            }

            float deltaProgress = deltaTime / task.Duration;
            task.Progress += task.IsPlayingForward ? deltaProgress : -deltaProgress;
            
            if (task.IsPlayingForward && task.Progress >= 1f)
            {
                if (task.Mirror)
                {
                    task.Progress = 1f;
                    task.IsPlayingForward = false;
                }
                else
                {
                    task.IsCompleted = true;
                    
                    if (task.Removable) _tasks.RemoveAt(i);
                }
            }
            else if (!task.IsPlayingForward && task.Progress <= 0f)
            {
                task.Progress = 0f;
                task.IsCompleted = true;
                
                if (task.Removable) _tasks.RemoveAt(i);
            }
        }
    }

    // required call in draw
    public void Draw()
    {
        foreach (var task in _tasks)
        {
            if (task.Delay > 0f) continue;
            task.Action(task.Progress);
        }
    }
}