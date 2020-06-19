using System;
using System.Collections.Generic;
using TGPR.Database.Common.Models.Users;

namespace TGPR.Database.Common.Models.Applications
{
    public class ApplicationModel
    {
        public int ApplicationId { get; set; }
        public int ApplicationTemplateId { get; set; }
        public int ApplicationStatusId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsTest { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedById { get; set; }

        public ApplicationTemplateModel ApplicationTemplate { get; set; }

        public ApplicationStatusModel ApplicationStatus { get; set; }

        public UserModel UpdatedBy { get; set; }

        public IEnumerable<ApplicationQuestionAnswerModel> QuestionAnswers { get; set; }
        
        public IEnumerable<ApplicationCategoryReviewTextModel> ApplicationCategoryReviews { get; set; }
    }
}
