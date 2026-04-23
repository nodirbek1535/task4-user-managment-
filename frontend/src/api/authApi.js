import axiosClient from './axiosClient';

export const authApi = {
  register: (data) => axiosClient.post('/auth/register', data),
  login: (data) => axiosClient.post('/auth/login', data),
  confirmEmail: (token) => axiosClient.get(`/auth/confirm-email?token=${token}`),
};
