import axios from 'axios';

export const activitiesApi = () => {
    // We would deal with auth here (JWT) if we had login
    return axios.create({
        baseURL: '/api/Activity'
    });
};