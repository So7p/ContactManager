import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import ContactsPage from './ContactsPage';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<ContactsPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;