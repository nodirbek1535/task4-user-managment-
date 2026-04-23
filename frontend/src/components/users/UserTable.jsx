import React from 'react';
import UserRow from './UserRow';
import SelectAllCheckbox from './SelectAllCheckbox';

const UserTable = ({ 
  users, 
  selectedIds, 
  onToggleId, 
  onToggleAll, 
  isAllSelected, 
  isSomeSelected 
}) => {
  if (!users || users.length === 0) {
    return <div className="p-4 text-center border-start border-end border-bottom rounded-bottom bg-white text-muted">No users found.</div>;
  }

  return (
    <div className="table-responsive border border-top-0 rounded-bottom bg-white">
      <table className="table table-hover mb-0">
        <thead className="table-light">
          <tr>
            <th scope="col" style={{ width: '40px' }}>
              <SelectAllCheckbox 
                isAllSelected={isAllSelected}
                isSomeSelected={isSomeSelected}
                onChange={(e) => {
                  if (e.target.checked) {
                    onToggleAll(users.map(u => u.id));
                  } else {
                    onToggleAll([]);
                  }
                }}
              />
            </th>
            <th scope="col">Name</th>
            <th scope="col">Email <i className="bi bi-arrow-down-short text-muted"></i></th>
            <th scope="col">Status</th>
            <th scope="col">Last seen</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <UserRow 
              key={user.id} 
              user={user} 
              isSelected={selectedIds.includes(user.id)}
              onToggle={onToggleId}
            />
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserTable;
