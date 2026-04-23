import React from 'react';
import StatusBadge from '../common/StatusBadge';
import { formatRelativeTime } from '../../utils/formatDate';

const UserRow = ({ user, isSelected, onToggle }) => {
  return (
    <tr className={isSelected ? 'table-active' : ''}>
      <td className="align-middle" style={{ width: '40px' }}>
        <input 
          type="checkbox" 
          className="form-check-input"
          checked={isSelected}
          onChange={() => onToggle(user.id)}
          aria-label={`Select user ${user.name}`}
        />
      </td>
      <td className="align-middle">
        <div className="fw-bold">{user.name || 'N/A'}</div>
      </td>
      <td className="align-middle">{user.email}</td>
      <td className="align-middle">
        <StatusBadge status={user.status} />
      </td>
      <td className="align-middle text-muted small">
        {formatRelativeTime(user.lastLoginTime)}
      </td>
    </tr>
  );
};

export default UserRow;
