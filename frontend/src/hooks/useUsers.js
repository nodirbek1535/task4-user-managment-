import { useState, useCallback } from 'react';
import { userApi } from '../api/userApi';
import { toast } from 'react-toastify';

export const useUsers = () => {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(false);

  const fetchUsers = useCallback(async () => {
    setLoading(true);
    try {
      const response = await userApi.getAll();
      setUsers(response.data);
    } catch (error) {
      toast.error('Failed to fetch users');
      console.error(error);
    } finally {
      setLoading(false);
    }
  }, []);

  const handleBulkAction = async (actionFn, userIds, successMessage) => {
    if (userIds.length === 0) return;
    
    setLoading(true);
    try {
      await actionFn(userIds);
      toast.success(successMessage);
      await fetchUsers();
      return true;
    } catch (error) {
      const message = error.response?.data?.title || error.message || 'Operation failed';
      toast.error(message);
      return false;
    } finally {
      setLoading(false);
    }
  };

  const blockUsers = (userIds) => handleBulkAction(userApi.block, userIds, 'Users blocked successfully');
  const unblockUsers = (userIds) => handleBulkAction(userApi.unblock, userIds, 'Users unblocked successfully');
  const deleteUsers = (userIds) => handleBulkAction(userApi.delete, userIds, 'Users deleted successfully');
  const deleteUnverified = (userIds) => handleBulkAction(userApi.deleteUnverified, userIds, 'Unverified users deleted successfully');

  return {
    users,
    loading,
    fetchUsers,
    blockUsers,
    unblockUsers,
    deleteUsers,
    deleteUnverified
  };
};
