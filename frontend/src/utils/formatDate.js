import { formatDistanceToNow, parseISO } from 'date-fns';

export const formatRelativeTime = (dateString) => {
  if (!dateString) return 'Never';
  
  try {
    const date = parseISO(dateString);
    return formatDistanceToNow(date, { addSuffix: true });
  } catch (error) {
    console.error('Date parsing error:', error);
    return 'Unknown';
  }
};
