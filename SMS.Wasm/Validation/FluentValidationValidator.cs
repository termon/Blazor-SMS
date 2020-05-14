using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.Extensions.DependencyInjection;
using SMS.Wasm.Services;

namespace SMS.Wasm.Validation
{
    public class FluentValidationValidator : ComponentBase
    {
        [CascadingParameter] EditContext CurrentEditContext { get; set; }

        // amc - added to provide access to DI
        // calling async functions makes browser un-responsive even if block async call using .Result
        [Inject] private IServiceProvider ServiceProvider { get; set; }
       
        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(FluentValidationValidator)} requires a cascading " +
                    $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(FluentValidationValidator)} " +
                    $"inside an {nameof(EditForm)}.");
            }
            Console.WriteLine("Adding fluent validation");
            CurrentEditContext.AddFluentValidation();
        }
    }

    // extension methods
    public static class EditContextFluentValidationExtensions
    {
        public static EditContext AddFluentValidation(this EditContext editContext)
        {
            if (editContext == null)
            {
                throw new ArgumentNullException(nameof(editContext));
            }

            var messages = new ValidationMessageStore(editContext);

            editContext.OnValidationRequested +=
                (sender, eventArgs) => ValidateModel((EditContext)sender, messages);

            editContext.OnFieldChanged +=
                (sender, eventArgs) => ValidateField(editContext, messages, eventArgs.FieldIdentifier);

            return editContext;
        }

        private static void ValidateModel(EditContext editContext, ValidationMessageStore messages)
        {       
            // AMC added try-catch to handle case when there is no validator for model     
            try {
                var validator = GetValidatorForModel(editContext.Model);
                var validationResults = validator.Validate(editContext.Model);

                messages.Clear();
                foreach (var validationResult in validationResults.Errors)
                {
                    messages.Add(editContext.Field(validationResult.PropertyName), validationResult.ErrorMessage);
                }
                editContext.NotifyValidationStateChanged();
            } catch (Exception) { 
                Console.WriteLine ($"No Validator Available for {editContext.Model.GetType().ToString()}");
            }
        }

        private static void ValidateField(EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier)
        {
            var properties = new[] { fieldIdentifier.FieldName };
            var context = new ValidationContext(fieldIdentifier.Model, new PropertyChain(), new MemberNameValidatorSelector(properties));

            var validator = GetValidatorForModel(fieldIdentifier.Model);
            var validationResults = validator.Validate(context);

            messages.Clear(fieldIdentifier);

            foreach (var validationResult in validationResults.Errors)
            {
                messages.Add(editContext.Field(validationResult.PropertyName), validationResult.ErrorMessage);
            }

            editContext.NotifyValidationStateChanged();
        }

        
        private static IValidator GetValidatorForModel(object model)
        {
            var abstractValidatorType = typeof(AbstractValidator<>).MakeGenericType(model.GetType());
            var modelValidatorType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.IsSubclassOf(abstractValidatorType));
            var modelValidatorInstance = (IValidator)Activator.CreateInstance(modelValidatorType);
         
            return modelValidatorInstance;
        }
    }

}