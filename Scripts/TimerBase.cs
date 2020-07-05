using CM.Events;
using System;

namespace CM.Timing
{
	/// <summary>
	/// Represents the base of a Start/Stop Timer with a given time in seconds.
	/// </summary>
	public abstract class TimerBase
	{
		/// <summary>
		/// An event for when the timer finishes.
		/// </summary>
		public event SimpleEvent OnFinish;

		/// <summary>
		/// The total time to use for the timer.
		/// </summary>
		public float TotalTime
		{
			get
			{
				return _totalTime;
			}
			set
			{
				if (value <= 0)
					throw new Exception("TotalTime can't be less than or equal to zero.");

				_totalTime = value;
			}
		}

		/// <summary>
		/// The current time of the timer.
		/// </summary>
		public float CurrentTime
		{
			get
			{
				return _currentTime;
			}
			set
			{
				_currentTime = Math.Max(value, 0);
			}
		}

		/// <summary>
		/// Is the timer running?
		/// </summary>
		public bool IsRunning { get; private set; }

		private float _totalTime = 0f;
		private float _currentTime = 0f;

		/// <summary>
		/// Constructor for a timer with a TotalTime of zero.
		/// </summary>
		public TimerBase()
		{
			
		}

		/// <summary>
		/// Constructor for a timer with a specified time in seconds.
		/// </summary>
		/// <param name="time">The time in seconds that this timer needs to run before finish.</param>
		public TimerBase(float time)
		{
			TotalTime = time;
			Reset();
		}

		protected abstract void OnStart();
		protected abstract void OnStop();

		/// <summary>
		/// Starts running the timer.
		/// </summary>
		public void Start()
		{
			if (IsRunning)
				return;

			IsRunning = true;

			OnStart();
		}

		/// <summary>
		/// Stops running the timer.
		/// </summary>
		public void Stop()
		{
			if (!IsRunning)
				return;

			IsRunning = false;

			OnStop();
		}

		/// <summary>
		/// Resets the timer back to the TotalTime.
		/// </summary>
		public void Reset()
		{
			CurrentTime = TotalTime;
		}

		protected void InvokeOnFinish()
		{
			OnFinish?.Invoke();
		}
	}
}
