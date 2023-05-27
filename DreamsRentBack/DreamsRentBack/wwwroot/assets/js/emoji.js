$(document).ready(function() {
    $("#emojiButton").click(function() {
      // Open emoji picker
      $("#emojiTextArea").emojioneArea();
    });
  });
  $(document).ready(function() {
    $("#emojiTextArea").emojioneArea({
      events: {
        keyup: function(editor, event) {
          var emoji = event.target.value;
          $("#emojiTextArea").val($("#emojiTextArea").val() + emoji);
        }
      }
    });
  });
  $(document).ready(function() {
    $("#emojiButton").click(function() {
      $("#emojiTextArea").emojioneArea({
        pickerPosition: "top",
        tones: false,
        autocomplete: false,
        inline: false,
        hidePickerOnBlur: true
      });
    });

    $("#emojiTextArea").on("paste", function(e) {
      e.preventDefault();
      var text = (e.originalEvent || e).clipboardData.getData("text/plain");
      document.execCommand("insertText", false, text);
    });
  });       