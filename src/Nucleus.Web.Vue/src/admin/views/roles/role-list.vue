<template>
    <div>
        <h1>
            Roles
            <b-btn v-b-modal.modalCreateOrUpdateRole
                   @click="setGetRoleForCreateOrUpdateInput()"
                   variant="success"
                   class="float-right">
                Create Role
            </b-btn>
        </h1>

        <div v-if="pagedListOfRoleListDto.totalCount">
            <table class="table">
                <thead>
                    <tr>
                        <th>Actions</th>
                        <th>Role Name</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in pagedListOfRoleListDto.items" :key="item.id">
                        <td>
                            <div class="btn-group">
                                <button v-if="!item.isSystemDefault"
                                        v-on:click="remove(item.id)"
                                        class="btn btn-danger btn-sm">
                                    <i class="fas fa-trash-alt"></i>
                                </button>
                                <b-btn v-b-modal.modalCreateOrUpdateRole
                                       v-if="!item.isSystemDefault"
                                       @click="setGetRoleForCreateOrUpdateInput(item)"
                                       variant="primary"
                                       size="sm">
                                    <i class="fas fa-pen"></i>
                                </b-btn>
                            </div>
                        </td>
                        <td>{{ item.name }}</td>
                    </tr>
                </tbody>
            </table>
            <b-pagination align="right" 
                          :total-rows="pagedListOfRoleListDto.totalCount" 
                          v-model="currentPage" 
                          :per-page="10" 
                          @input="changePage">
            </b-pagination>
        </div>

        <p v-else><em>Loading...</em></p>

        <create-or-update-role-modal-component />
    </div>
</template>

<script src="./role-list.ts"></script>
