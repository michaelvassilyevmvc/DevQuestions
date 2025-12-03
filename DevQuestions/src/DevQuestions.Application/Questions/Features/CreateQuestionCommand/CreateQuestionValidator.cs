using DevQuestions.Contracts.Questions.Dtos;
using FluentValidation;

namespace DevQuestions.Application.Questions.Features.CreateQuestionCommand;

public class CreateQuestionValidator: AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Заголовок не должен быть пустым")
            .MaximumLength(500).WithMessage("Заголовок не валидный.");
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Текст не должен быть пустым")
            .MaximumLength(5000).WithMessage("Текст не валидный");
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleForEach(x => x.TagIds)
            .NotEmpty();
    }
    
}