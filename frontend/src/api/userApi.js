import axiosClient from './axiosClient';

export const userApi = {
  getAll: () => axiosClient.get('/user'),
  block: (userIds) => axiosClient.patch('/user/block', userIds),
  unblock: (userIds) => axiosClient.patch('/user/unblock', userIds),
  delete: (userIds) => axiosClient.delete('/user/bulk', { data: userIds }),
  deleteUnverified: (userIds) => axiosClient.delete('/user/unverified', { data: userIds }),
};
