using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Interfaces.Model.Reviewer;

public class AddReviewerRequest
{
	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string SureName { get; set; }

	[Required]
	public string Email { get; set; }
}