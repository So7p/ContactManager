import React, { useState, useEffect } from 'react';
import './ContactsPage.css';
import axios from 'axios';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button, Dialog, DialogTitle, DialogContent, Stack, TextField, TablePagination } from '@mui/material';

const ContactsPage = () => {
    const [contacts, setContacts] = useState([]);
    const [selectedContact, setSelectedContact] = useState([]);

    const columns = [
        { id: 'name', name: 'Name' },
        { id: 'mobilePhone', name: 'Mobile phone' },
        { id: 'jobTitle', name: 'Job title' },
        { id: 'birthDate', name: 'Date of Birth' },
        { id: 'actions', name: 'Actions' }
    ]

    const fetchContacts = async () => {
        try {
            const response = await axios.get('https://localhost:7014/api/Contact');
            setContacts(response.data);
        }
        catch (error) {
            console.error('An error occurred while attempting to load contacts: ', error);
        }
    };

    useEffect(() => {
        fetchContacts();
    }, []);

    const selectContact = (contact) => {
        setSelectedContact(contact);
    };

    const[name, nameChange] = useState('');
    const[mobilePhone, mobilePhoneChange] = useState('');
    const[jobTitle, jobTitleChange] = useState('');
    const[birthDate, birthDateChange] = useState('');

    const[rowPerPage, rowPerPageChange] = useState(5);
    const[page, pageChange] = useState(0);

    const[open, openChange] = useState(false);
    const[openDelete, openDeleteChange] = useState(false);

    const[isEdit, isEditChange] = useState(false);
    const[title, titleChange] = useState('Create Contact');
    const[deletionTitle, deletionTitleChange] = useState('');

    const validateInputs = () => {
        if (name.length > 30) {
            console.error('Name should be no more than 30 characters');
            return false;
        }
        if (!/^\+(?:[0-9]){6,14}[0-9]$/.test(mobilePhone)) {
            console.error('Mobile phone must match the specified format');
            return false;
        }
        if (jobTitle.length > 30) {
            console.error('Job title should be no more than 30 characters');
            return false;
        }
        if (new Date().getFullYear() - new Date(birthDate).getFullYear() < 16) {
            console.error('Contact must be at least 16 years old');
            return false;
        }
        return true;
    };

    const openPopup = () => {
        openChange(true);
    };

    const closePopup = () => {
        openChange(false);

        nameChange('');
        mobilePhoneChange('');
        jobTitleChange('');
        birthDateChange('');
    };

    const openCreateContactPopup = (contact) => {
        isEditChange(false);
        titleChange('Create Contact');
        openPopup();   
        mobilePhoneChange('+');
    };

    const openUpdateContactPopup = (contact) => {
        selectContact(contact);

        const selectedContactData = contacts.find((c) => c.id === contact.id)

        if (selectedContactData) {
            nameChange(selectedContactData.name);
            mobilePhoneChange(selectedContactData.mobilePhone);
            jobTitleChange(selectedContactData.jobTitle);
            birthDateChange(selectedContactData.birthDate);

            isEditChange(true);
            titleChange('Update Contact');
            openChange(true);
        }
        else {
            console.error('No contact selected');
        } 
    };

    const openDeleteContactPopup = (contact) => { 
        selectContact(contact);

        const selectedContactData = contacts.find((c) => c.id === contact.id)

        if (selectedContactData) {
            deletionTitleChange(selectedContactData.name);
            openDeleteChange(true);
        }   
         else {
            console.error('No contact selected');
        }    
    };

    const closeDeleteContactPopup =() => { 
        openDeleteChange(false);
        nameChange('');
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (validateInputs()) { 
            if (isEdit) {
                await updateContact();
                closePopup();
            }
            else {
                await createContact();
                closePopup();
            }
    
            fetchContacts();
        }   
    };

    const handleDeleteConfirmation = async (e) => {
        e.preventDefault();
        await deleteContact();
        closeDeleteContactPopup();

        fetchContacts();
    };

    const handlePageChange = (event, newPage) => {
        pageChange(newPage);
    };

    const handleRowsPerPageChange = (event) => {
        rowPerPageChange(+event.target.value);
        pageChange(0);
    };

    const createContact = async () => {
        try {
            const newContact = {
                Name: name,
                MobilePhone: mobilePhone,
                JobTitle: jobTitle,
                BirthDate: birthDate
            }

            const response = await axios.post(`https://localhost:7014/api/Contact/create`, newContact,
                {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                    }
                }
            );

            console.log('Contact created successfully:', response.data);

            setContacts([...contacts, response.data]);
        }
        catch (error) {
            if (error.response) {
                console.error('Server Error:', error.response.data);
            } else if (error.request) {
                console.error('No response received:', error.request);
            } else {
                console.error('Error setting up the request:', error.message);
            }
        }
    };

    const updateContact = async () => {
        try {
            const updatedContact = {
                Id: selectedContact.id,
                Name: name,
                MobilePhone: mobilePhone,
                JobTitle: jobTitle,
                BirthDate: birthDate
            }
            
            const response = await axios.put(`https://localhost:7014/api/Contact/update`, updatedContact,
                {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                    }
                }
            );

            console.log('Contact updated successfully:', response.data);

            setContacts([...contacts, response.data]);
            setSelectedContact(null)
        }
        catch (error) {
            if (error.response) {
                console.error('Server Error:', error.response.data);
            } else if (error.request) {
                console.error('No response received:', error.request);
            } else {
                console.error('Error setting up the request:', error.message);
            }
        }
    };

    const deleteContact = async (contactId) => {
        try {
            contactId = selectedContact.id;

            const response = await axios.delete(`https://localhost:7014/api/Contact/delete/${contactId}`,
                {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                    }
                }
            );
            
            setContacts([...contacts, response.data]);
            setSelectedContact(null);
        } 
        catch (error) {
            if (error.response) {
                console.error('Server Error:', error.response.data);
            } else if (error.request) {
                console.error('No response received:', error.request);
            } else {
                console.error('Error setting up the request:', error.message);
            }
        }
    };

    return (
        <div>
            <h1>Contacts List</h1>
            <div>
                <Paper class="paper">
                    <div class="new-contact-button">
                        <Button onClick={openCreateContactPopup} variant='contained'>New Contact</Button>
                    </div>
                    <TableContainer class="table-container">
                        <Table class="contacts-table">
                            <TableHead class="table-head">
                                <TableRow class="table-head-row">
                                    {columns.map((column) =>
                                        <TableCell class="table-head-cell" key={column.id}>{column.name}</TableCell>
                                    )}
                                </TableRow>
                            </TableHead>
                            <TableBody class="table-body">
                                {contacts.slice(page*rowPerPage, page*rowPerPage+rowPerPage).map((contact) => (
                                    <TableRow class="table-body-row" key={contact.id} onClick={() => selectContact(contact)}>
                                        <TableCell class="table-body-cell">{contact.name}</TableCell>
                                        <TableCell class="table-body-cell">{contact.mobilePhone}</TableCell>
                                        <TableCell class="table-body-cell">{contact.jobTitle}</TableCell>
                                        <TableCell class="table-body-cell">{contact.birthDate}</TableCell>
                                        <TableCell class="table-body-cell actions">
                                            <Button onClick={() => openUpdateContactPopup(contact)} variant='contained' color='primary'>Edit</Button>
                                            <Button onClick={() => openDeleteContactPopup(contact)} variant='contained' color='error'>Delete</Button>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                    <TablePagination
                        rowsPerPageOptions={[5, 10, 15, 20]}
                        rowsPerPage={rowPerPage}
                        page={page}
                        count={contacts.length}
                        component={'div'}
                        onPageChange={handlePageChange}
                        onRowsPerPageChange={handleRowsPerPageChange}
                        class="pagination"
                    >
                    </TablePagination>
                </Paper>

                <Dialog open={open} onClose={closePopup}>
                    <DialogTitle>
                        <span>{title}</span>
                    </DialogTitle>
                    <DialogContent>
                        <form onSubmit={handleSubmit}>
                            <Stack spacing={2} margin={2}>
                                <TextField 
                                    required 
                                    error={name.length===0 || name.length > 30} 
                                    helperText={name.length > 30 ? 'Name should be no more than 30 characters' : ''}
                                    value={name} 
                                    onChange={e=>{nameChange(e.target.value)}} 
                                    variant='outlined' 
                                    label='Name'
                                />
                                <TextField 
                                    required
                                    error={name.length===0 || !/^\+(?:[0-9]){6,14}[0-9]$/.test(mobilePhone)}
                                    helperText={!/^\+(?:[0-9]){6,14}[0-9]$/.test(mobilePhone) ? 'Wrong mobile phone format' : ''}
                                    value={mobilePhone} 
                                    onChange={e=>{mobilePhoneChange(e.target.value)}} 
                                    variant='outlined' 
                                    label='Mobile phone'
                                />
                                <TextField 
                                    required 
                                    error={name.length===0 || jobTitle.length > 30} 
                                    helperText={jobTitle.length > 30 ? 'Job title should be no more than 30 characters' : ''}
                                    value={jobTitle} 
                                    onChange={e=>{jobTitleChange(e.target.value)}} 
                                    variant='outlined' 
                                    label='Job title'
                                />
                                <TextField 
                                    required 
                                    error={name.length===0 || new Date().getFullYear() - new Date(birthDate).getFullYear() < 16}
                                    helperText={new Date().getFullYear() - new Date(birthDate).getFullYear() < 16 ? 'Contact must be at least 16 years old' : ''}  
                                    value={birthDate} 
                                    onChange={e=>{birthDateChange(e.target.value)}} 
                                    variant='outlined' 
                                    label='Date of Birth'
                                />

                                <Button type='submit' variant='contained'>Submit</Button>
                                <Button onClick={closePopup} variant='contained' color='error'>Cancel</Button>
                            </Stack>
                        </form>
                    </DialogContent>
                </Dialog>

                <Dialog open={openDelete} onClose={closeDeleteContactPopup}>
                    <DialogTitle>
                        <span>Delete contact {deletionTitle}?</span>
                    </DialogTitle>
                    <DialogContent>
                        <form onSubmit={handleDeleteConfirmation}>
                            <Stack spacing={2} margin={2}>
                                <Button type='submit' variant='contained'>Delete</Button>
                                <Button onClick={closeDeleteContactPopup} variant='contained' color='error'>Cancel</Button>
                            </Stack>
                        </form>  
                    </DialogContent> 
                </Dialog>
            </div>
        </div>
    );
};

export default ContactsPage;