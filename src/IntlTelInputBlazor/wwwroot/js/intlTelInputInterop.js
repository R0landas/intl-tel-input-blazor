const inputs = [];

export function init(element, options) {
    const iti = window.intlTelInput(element, options);
    inputs.push(iti);
    return inputs.indexOf(iti);
}

export function get(id) {
    const input = inputs[id];
    
    const number = input.getNumber();
    const isValid = input.isValidNumber();
    const validationError = input.getValidationError();
    const countryData = input.getSelectedCountryData();
    const extension = input.getExtension();
    const numberType = input.getNumberType();
    
    return {isValid, number, validationError, countryData, extension, numberType};
}

export function setNumber(id, number) {
    const input = inputs[id];
    input.setNumber(number);
}