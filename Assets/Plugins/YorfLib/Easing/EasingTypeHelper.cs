namespace YorfLib
{
	internal class EasingTypeHelper
	{
		internal delegate float EasingDelegate(float time);

		internal readonly static EasingDelegate[] DELEGATES = new EasingDelegate[]
		{
		EasingFunctions.Linear,

		EasingFunctions.Quadratic.In,
		EasingFunctions.Quadratic.Out,
		EasingFunctions.Quadratic.InOut,

		EasingFunctions.Cubic.In,
		EasingFunctions.Cubic.Out,
		EasingFunctions.Cubic.InOut,

		EasingFunctions.Quartic.In,
		EasingFunctions.Quartic.Out,
		EasingFunctions.Quartic.InOut,

		EasingFunctions.Quintic.In,
		EasingFunctions.Quintic.Out,
		EasingFunctions.Quintic.InOut,

		EasingFunctions.Sinusoidal.In,
		EasingFunctions.Sinusoidal.Out,
		EasingFunctions.Sinusoidal.InOut,

		EasingFunctions.Exponential.In,
		EasingFunctions.Exponential.Out,
		EasingFunctions.Exponential.InOut,

		EasingFunctions.Circular.In,
		EasingFunctions.Circular.Out,
		EasingFunctions.Circular.InOut,

		EasingFunctions.Elastic.In,
		EasingFunctions.Elastic.Out,
		EasingFunctions.Elastic.InOut,

		EasingFunctions.Back.In,
		EasingFunctions.Back.Out,
		EasingFunctions.Back.InOut,

		EasingFunctions.Bounce.In,
		EasingFunctions.Bounce.Out,
		EasingFunctions.Bounce.InOut,
		};
	}
}
