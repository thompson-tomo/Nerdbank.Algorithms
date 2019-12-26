﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace NerdBank.Algorithms.NodeConstraintSelection
{
	using System.Collections.Generic;
	using System.Collections.Immutable;

	/// <summary>
	/// Object pooling for <see cref="Scenario"/> objects.
	/// </summary>
	internal class ScenarioPool
	{
		private readonly Stack<Scenario> bag = new Stack<Scenario>();
		private readonly IReadOnlyList<object> nodes;
		private readonly IReadOnlyDictionary<object, int> nodeIndex;

		/// <summary>
		/// Initializes a new instance of the <see cref="ScenarioPool"/> class.
		/// </summary>
		/// <param name="nodes">The nodes in the problem/solution.</param>
		internal ScenarioPool(IReadOnlyList<object> nodes)
		{
			this.nodes = nodes;
			this.nodeIndex = Scenario.CreateNodeIndex(nodes);
		}

		/// <summary>
		/// Acquires a recycled or new <see cref="Scenario"/> instance.
		/// </summary>
		/// <returns>An instance of <see cref="Scenario"/>.</returns>
		internal Scenario Take()
		{
			if (this.bag.Count > 0)
			{
				return this.bag.Pop();
			}

			return new Scenario(this.nodes, this.nodeIndex);
		}

		/// <summary>
		/// Returns a <see cref="Scenario"/> for recycling.
		/// </summary>
		/// <param name="scenario">The instance to recycle.</param>
		internal void Return(Scenario scenario) => this.bag.Push(scenario);
	}
}
