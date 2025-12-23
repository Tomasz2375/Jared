window.auth = {
    login: async function (dto) {
        const response = await fetch('/authorization/login', {
            method: 'POST',
            credentials: 'include',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(dto)
        });
        return response.ok;
    },
    logout: async function () {
        const response = await fetch('/authorization/logout', {
            method: 'POST',
            credentials: 'include'
        });
        return response.ok;
    }
};
