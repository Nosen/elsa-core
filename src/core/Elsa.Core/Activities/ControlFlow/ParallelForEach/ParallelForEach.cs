using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Services;
using Elsa.Services.Models;

// ReSharper disable once CheckNamespace
namespace Elsa.Activities.ControlFlow
{
    [ActivityDefinition(
        Category = "Control Flow",
        Description = "Iterate over a collection in parallel.",
        Icon = "far fa-circle",
        Outcomes = new[] { OutcomeNames.Iterate, OutcomeNames.Done }
    )]
    public class ParallelForEach : Activity
    {
        [ActivityProperty(Hint = "A collection of items to iterate over.")]
        public ICollection<object> Items { get; set; } = new Collection<object>();

        protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
        {
            var items = Items.Reverse().ToList();
            var results = new List<IActivityExecutionResult> { Done() };
            results.AddRange(items.Select(x => Outcome(OutcomeNames.Iterate, x)));
            return Combine(results);
        }
    }
}