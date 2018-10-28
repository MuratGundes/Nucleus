import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import UserAppService from '../../../services/user-app-service';

@Component({
    components: {
        CreateUserModalComponent: require('./create-user-modal.vue').default,
    }
})
export default class UserListComponent extends Vue {
    public pagedListOfUserListDto: IPagedList<IUserListInput> = {
        totalCount: 0,
        items: [],
    };
    public userAppService = new UserAppService();

    public mounted() {
        this.getUsers();
    }

    public getUsers() {
        const userListInput: IUserListInput = {
            filter: '',
        };

        this.userAppService.getAll(userListInput).then((response) => {
            this.pagedListOfUserListDto = response.content as IPagedList<IUserListInput>;
        });
    }
}
