export function handleApiError(error: any): string[] {
  let errorMessages: string[] = [];

  if (error && error.error && error.error.errors) {
    const errors = error.error.errors;

    for (const key in errors) {
      if (errors.hasOwnProperty(key)) {
        errors[key].forEach((message: string) => {
          errorMessages.push(message);
        });
      }
    }
  }

  return errorMessages;
}
