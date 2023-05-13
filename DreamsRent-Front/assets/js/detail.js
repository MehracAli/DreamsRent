const dateInput = document.querySelectorAll('.date-now');
dateInput.forEach(Element=>{
    const now = new Date();
    Element.value = now.toISOString().slice(0, 10);
})

const timeInput = document.querySelectorAll('.time-now');
timeInput.forEach(Element=>{
    const now = new Date();
    Element.value = now.toTimeString().slice(0, 5);
})