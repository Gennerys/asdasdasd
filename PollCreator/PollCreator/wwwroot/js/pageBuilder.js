document.addEventListener('DOMContentLoaded', () => {

	var pollForm = document.getElementById("pollBuilderForm");
	var addOptionButton = document.getElementById("addPollOption");
	var publishPollButton = document.getElementById("publishPoll");
	var pollBuilderFormFields = document.getElementById("pollBuilderFromFields");
	var errorMessageHolder = document.getElementById("inputError");
	var linkContainer = document.getElementById("container");
	addOptionButton.addEventListener("click", createInput);
	publishPollButton.addEventListener("click", handlePublish);

	var optionNumber = 1;
	createInput();
	createInput();
	function createInput() {
	
		var identifier = optionNumber++;
		var inputField = document.createElement("INPUT");
		inputField.setAttribute("type", "text");
		inputField.setAttribute("id", `Option${identifier}`);
		inputField.setAttribute("name", `Option${identifier}`);
		inputField.setAttribute("placeholder","Your option goes here");
		inputField.setAttribute("class", "pollOptions");
		document.getElementById("options").appendChild(inputField);
		
		if (optionNumber === 11) {
			addOptionButton.disabled = true;
		}
	}

	function extractProperId(key) {
		return key.substring(6);
	}

	function getFormData() {
		var formData = new FormData(pollForm);
		var formDataContainer = {
			options: []
		};

		for (const [key, value] of formData.entries()) {
			if (key.startsWith("Option")) {
				formDataContainer["options"].push({ SerialNumber: extractProperId(key), Value: value });
			}
			formDataContainer[key] = value;
		}
		console.log(formDataContainer);

		var formattedFormData = jsonObjectFormatter(formDataContainer);
		console.log(formattedFormData);

		return formattedFormData;
	}

	function jsonObjectFormatter(jsonObjectToFormat) {

		var formattedFormData = {
			Title: jsonObjectToFormat["title"],
			IsSingleOption: jsonObjectToFormat["pollType"],
			Options: jsonObjectToFormat["options"],
			PollId: jsonObjectToFormat["PollId"],
			EditorToken: jsonObjectToFormat["EditorToken"],
			__RequestVerificationToken: jsonObjectToFormat["__RequestVerificationToken"]
		};

		return formattedFormData;
	}

	function createVotingPageLink(pollId) {

		var votingPageLink = document.createElement('a');
		votingPageLink.setAttribute("href", `/p/${pollId}`);
		var linkText = document.createTextNode("Poll Voting Page");
		votingPageLink.appendChild(linkText);
		linkContainer.appendChild(votingPageLink);
		createHiddenLinkInput(votingPageLink.href);
	}

	function createCopyUrlButton() {

		var copyToClipboardBtn = document.createElement("button");
		copyToClipboardBtn.setAttribute("type", "button");
		var buttonText = document.createTextNode("Copy Link");
		copyToClipboardBtn.appendChild(buttonText);
		linkContainer.appendChild(copyToClipboardBtn);
		copyToClipboardBtn.addEventListener("click", () => {
			var hiddenLink = document.getElementById("hiddenLink");
			hiddenLink.focus();
			hiddenLink.select();
			document.execCommand("copy");
		});
	}



	function createHiddenLinkInput(url) {

		var inputField = document.createElement("INPUT");
		inputField.setAttribute("id","hiddenLink");
		inputField.setAttribute("type", "text");
		inputField.style.top = "-9999px";
		inputField.style.position = "absolute";
		inputField.setAttribute("value", `${url}`);
		linkContainer.appendChild(inputField);
		console.log(inputField);
	}

	function handlePublish(event) {

		var isFormValid = validateForm();
		if (isFormValid) {
			errorMessageHolder.innerHTML = "";
			var formDataJson = getFormData();
			var formDataToSend = JSON.stringify(formDataJson);
			pollBuilderFormFields.disabled = true;
			var xmlHttpRequest = new XMLHttpRequest();
			xmlHttpRequest.onreadystatechange = function() {
				if (this.readyState === 4 && this.status === 200) {
					createVotingPageLink(formDataJson.PollId);
					createCopyUrlButton();
				} 
			};

			xmlHttpRequest.open('POST', '/p/publish', true);
			xmlHttpRequest.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
			xmlHttpRequest.send(formDataToSend);
		}

	}

	function validateForm() {

		var formFields = pollForm.getElementsByTagName("input");
		for (var i = 0; i < formFields.length; i++) {
			if (formFields[i].value === "") {
				errorMessageHolder.innerHTML = `Input error in ${formFields[i].name}`;
				return false;
			}
		}
		return true;

	}

});

	
